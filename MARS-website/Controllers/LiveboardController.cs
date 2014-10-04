using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Mars2.Data;
using MARS.Utilities;
using MARS.Web;
using MARS.Web.Models;
using MARS.Meet;

namespace MARS.Web.Controllers
{
	public class MyLogger
	{
		public static void Log(string component, string message)
		{
			//if (LogManager.Path.IsNull())
			//{
			//	LogManager.Path = @"C:\WorkingMARS-SqlServer";
			//	LogManager.AutoAddTime = true;
			//}

			//LogManager.Save("Component: {0} Message: {1} ", component, message);
		}
	}

	public class LiveboardController : Controller
	{
		// GET: /Liveboard/
		public ActionResult Index()
		{
			MeetModel model = new MeetModel() { MeetList = new List<OneMeet>() };

			using (MARS_Meet_dbEntities meetDb = new MARS_Meet_dbEntities())
			{
				DateTime d = new DateTime(2014, 01, 01);

				foreach (var meet in from mars_Meet in meetDb.mars_MMeet where mars_Meet.StartDate >= d orderby mars_Meet.StartDate descending select mars_Meet)
					model.MeetList.Add(createMeet(meet, Selection.Events));
			}

			return View(model);
		}

		// GET: /Liveboard/Events/
		public ActionResult Events(Guid meetId, string groupId = null, int dayNo = 0)
		{
			using (MARS_Meet_dbEntities meetDb = new MARS_Meet_dbEntities())
			{
				meetDb.Database.Log = s => MyLogger.Log("Events", s);

				mars_MMeet meet = (from mars_Meet in meetDb.mars_MMeet where mars_Meet.MeetId == meetId select mars_Meet).SingleOrDefault();
				if (meet == null)
					return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

				OneMeet oneMeet = createMeet(meet, Selection.Events);
				oneMeet.CurrentGroupId = groupId;
				oneMeet.CurrentEventId = Guid.Empty;
				oneMeet.CurrentDay = dayNo;

				loadEvents(meetDb, oneMeet);

				return View(oneMeet);
			}
		}

		// GET: /Liveboard/Event/
		public ActionResult Event(Guid meetId, Guid eventId, string groupId = null, int dayNo = 0)
		{
			using (MARS_Meet_dbEntities meetDb = new MARS_Meet_dbEntities())
			{
				meetDb.Database.Log = s => MyLogger.Log("Event", s);

				mars_MMeet meet = (from m in meetDb.mars_MMeet where m.MeetId == meetId select m).SingleOrDefault();
				if (meet == null)
					return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

				mars_MEventEntries query = (from e in meetDb.mars_MEventEntries where e.MeetId == meetId && e.EventEntryId == eventId select e).SingleOrDefault();

				if (query == null)
					return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

				OneMeet oneMeet = createMeet(meet, Selection.OneEvent);
				oneMeet.CurrentGroupId = groupId;
				oneMeet.CurrentEventId = eventId;
				oneMeet.CurrentDay = dayNo;

				loadEvents(meetDb, oneMeet);

				if (query.LiveResults.IsNotNull())
				{
					List<mars_MTeams> q = (from t in meetDb.mars_MTeams where t.MeetId == meetId select t).ToList();

					OneEvent oneEvent = oneMeet.Events[0];
					oneEvent.Staevne = XmlHelper<Staevne>.GetObject(query.LiveResults);
					Mars2Loader.AfterLoad(oneEvent.Staevne);

					oneEvent.BuildRecordList();
					oneEvent.BuildStartList(q);
					oneEvent.BuildResultList(q);
				}

				return View(oneMeet);
			}
		}

		// GET: /Liveboard/Teams/
		public ActionResult Teams(Guid meetId, string groupId = null, int dayNo = 0)
		{
			using (MARS_Meet_dbEntities meetDb = new MARS_Meet_dbEntities())
			{
				meetDb.Database.Log = s => MyLogger.Log("Teams", s);

				mars_MMeet meet = (from mars_Meet in meetDb.mars_MMeet where mars_Meet.MeetId == meetId select mars_Meet).SingleOrDefault();
				if (meet == null)
					return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

				OneMeet oneMeet = createMeet(meet, Selection.Teams);
				oneMeet.CurrentGroupId = groupId;
				oneMeet.CurrentEventId = Guid.Empty;
				oneMeet.CurrentDay = dayNo;

				loadEvents(meetDb, oneMeet);
				loadTeams(meetDb, oneMeet);

				return View(oneMeet);
			}
		}

		// GET: /Liveboard/Team/
		public ActionResult Team(Guid meetId, Guid teamId, string groupId = null, int dayNo = 0, int sortBy = 0)
		{
			using (MARS_Meet_dbEntities meetDb = new MARS_Meet_dbEntities())
			{
				meetDb.Database.Log = s => MyLogger.Log("Team", s);

				mars_MMeet meet = (from mars_Meet in meetDb.mars_MMeet where mars_Meet.MeetId == meetId select mars_Meet).SingleOrDefault();
				if (meet == null)
					return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

				mars_MTeams query = (from team in meetDb.mars_MTeams
									 where team.MeetId == meetId && team.TeamId == teamId
									 select team).SingleOrDefault();

				if (query == null)
					return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

				OneMeet oneMeet = createMeet(meet, Selection.OneTeam);
				oneMeet.CurrentGroupId = groupId;
				oneMeet.CurrentDay = dayNo;
				oneMeet.CurrentSortBy = sortBy;

				oneMeet.CurrentTeam = new OneTeam()
				{
					Name = query.ShortName,
					Id = query.TeamId,
					Participants = new List<DispParticipant>()
				};

				loadEvents(meetDb, oneMeet);
				loadTeam(meetDb, oneMeet);

				return View(oneMeet);
			}
		}

		private OneMeet createMeet(mars_MMeet from, Selection selection)
		{
			return new OneMeet() { MeetId = from.MeetId, Description = from.Header, NoOfDays = from.Days, Selection = selection };
		}

		private void loadTeams(MARS_Meet_dbEntities meetDb, OneMeet oneMeet)
		{
			var query = from t in meetDb.mars_MTeams
						where t.MeetId == oneMeet.MeetId
						orderby t.ShortName
						select new
						{
							t.TeamId,
							t.ShortName
						};

			oneMeet.Teams = new List<OneTeam>();

			foreach (var t in query)
			{
				OneTeam team = new OneTeam
				{
					Id = t.TeamId,
					Name = t.ShortName
				};

				oneMeet.Teams.Add(team);
			}

			oneMeet.TeamRows = new List<List<OneTeam>>();
			List<OneTeam> current = null;

			int i = 0;
			int n = 3;  // 3 teams in each row
			while (i < oneMeet.Teams.Count)
			{
				if (i % n == 0)
				{
					current = new List<OneTeam>();
					oneMeet.TeamRows.Add(current);
				}
				current.Add(oneMeet.Teams[i++]);
			}
		}

		private void loadTeam(MARS_Meet_dbEntities meetDb, OneMeet oneMeet)
		{
			var athletes = from t in meetDb.mars_MTeams
						 join p in meetDb.mars_MParticipants on new { t.MeetId, t.TeamId } equals new { p.MeetId, p.TeamId }
						 join m in meetDb.mars_MMarks on new { p.MeetId, p.ParticipantId } equals new { m.MeetId, m.ParticipantId }
						 join e in meetDb.mars_MEventEntries on new { m.MeetId, m.EventEntryId } equals new { e.MeetId, e.EventEntryId }
						 join g in meetDb.mars_MGroups on new { e.MeetId, e.GroupId } equals new { g.MeetId, g.GroupId }
						 where t.MeetId == oneMeet.MeetId && t.TeamId == oneMeet.CurrentTeam.Id
							&& (oneMeet.CurrentDay == 0 || e.DayNo == oneMeet.CurrentDay) 
							&& (oneMeet.CurrentGroupId == null || g.GroupId == oneMeet.CurrentGroupId)
						 select new
						 {
							 p.ParticipantId,
							 p.FullName,
							 p.YearOfBirth,
							 p.BibNo,
							 e.EventEntryId,
							 e.GroupId,
							 e.EventId,
							 e.RoundNo,
							 e.NoOfRounds,
							 e.DayNo,
							 e.Seconds,
							 e.Text,
							 e.RoundText,
							 e.State,
							 GroupText = g.Text,
							 GroupSortKey = g.SortKey,
							 EventSortKey = e.SortKeyEventId,
							 m.MarkText,
							 m.MarkCode,
							 m.MarkRemark,
							 m.Pos
						 };

			var eventsInHeat0 = from ev in meetDb.mars_MEventEntries
						where ev.MeetId == oneMeet.MeetId && ev.HeatGroupNo == 0
   						    && (oneMeet.CurrentDay == 0 || ev.DayNo == oneMeet.CurrentDay)
							&& (oneMeet.CurrentGroupId == null || ev.GroupId == oneMeet.CurrentGroupId)
						select new
						{
							ev.EventEntryId,
							ev.GroupId,
							ev.EventId,
							ev.RoundNo
						};

			foreach (var p in athletes)
			{
				DispParticipant d = new DispParticipant();
				d.ParticipantId = p.ParticipantId;
				d.Name = p.FullName;
				d.YOB = p.YearOfBirth != null && p.YearOfBirth != 0 ? p.YearOfBirth.ToString() : "";
				d.BibNo = p.BibNo.ToString();

				d.Result = p.MarkText;
				d.ResultCode = p.MarkCode;
				d.Comment = p.MarkRemark;
				d.Position = p.Pos == null ? "" : p.Pos.ToString();

				Guid eventEntryId = (from x in eventsInHeat0 where x.EventId == p.EventId && x.GroupId == p.GroupId && x.RoundNo == p.RoundNo select x.EventEntryId).FirstOrDefault();

				d.OneEvent = new OneEvent
				{
					Id = eventEntryId,
	
					EventId = p.EventId,
					GroupId = p.GroupId,
					EventText = p.Text,
					GroupText = p.GroupText,
					RoundText = p.RoundText,
					RoundNo = p.RoundNo,
					NoOfRounds = (int)p.NoOfRounds,
					Day = (int)p.DayNo,
					Seconds = (int)p.Seconds,
					State = p.State,
					GroupSortKey = p.GroupSortKey,
					EventSortKey = p.EventSortKey
				};

				oneMeet.CurrentTeam.Participants.Add(d);
			}

			switch (oneMeet.CurrentSortBy)
			{
				case 0:
					oneMeet.CurrentTeam.Participants.Sort(CompareTimeSchedule);
					break;
				case 1:
					oneMeet.CurrentTeam.Participants.Sort(CompareGroupEvent);
					break;
				case 2:
					oneMeet.CurrentTeam.Participants.Sort(CompareEventGroup);
					break;
				case 3:
					oneMeet.CurrentTeam.Participants.Sort(CompareName);
					break;
			}

			Guid currentId = Guid.Empty;
			int number = 0;
			foreach (var d in oneMeet.CurrentTeam.Participants)
			{
				d.OrderNo = (++number).ToString();

				if (d.ParticipantId.Equals(currentId))
				{
					d.Name = "";
					d.YOB = "";
					d.BibNo = "";
				}

				currentId = d.ParticipantId;
			}
		}

		private void loadEvents(MARS_Meet_dbEntities meetDb, OneMeet oneMeet)
		{
			var query = from ev in meetDb.mars_MEventEntries
						join gr in meetDb.mars_MGroups on new { ev.MeetId, ev.GroupId } equals new { gr.MeetId, gr.GroupId }
						where ev.MeetId == oneMeet.MeetId && ev.HeatGroupNo == 0 && (oneMeet.CurrentDay == 0 || ev.DayNo == oneMeet.CurrentDay)
						orderby gr.SortKey, ev.SortKey, ev.RoundNo
						select new
						{
							ev.EventEntryId,
							ev.GroupId,
							ev.EventId,
							ev.RoundNo,
							ev.NoOfRounds,
							ev.DayNo,
							ev.Seconds,
							ev.Text,
							ev.RoundText,
							ev.State,
							GroupText = gr.Text,
							GroupSortKey = gr.SortKey,
							EventSortKey = ev.SortKeyEventId
						};

			oneMeet.Groups = new List<OneGroup>();
			oneMeet.Events = new List<OneEvent>();

			OneGroup currentGroup = null;

			foreach (var e in query)
			{
				if (currentGroup == null || currentGroup.GroupId != e.GroupId)
				{
					currentGroup = new OneGroup { GroupId = e.GroupId, GroupText = e.GroupText, SortKey = e.GroupSortKey, Events = new List<OneEvent>() };
					oneMeet.Groups.Add(currentGroup);
				}

				OneEvent t = new OneEvent
				{
					Id = e.EventEntryId,
					EventId = e.EventId,
					GroupId = e.GroupId,
					EventText = e.Text,
					GroupText = e.GroupText,
					RoundText = e.RoundText,
					RoundNo = e.RoundNo,
					NoOfRounds = (int)e.NoOfRounds,
					Day = (int)e.DayNo,
					Seconds = (int)e.Seconds,
					State = e.State,
					GroupSortKey = e.GroupSortKey,
					EventSortKey = e.EventSortKey
				};

				currentGroup.Events.Add(t);

				if (oneMeet.CurrentEventId == t.Id)
				{
					oneMeet.Events.Add(t);
				}
				else
				{
					if (oneMeet.CurrentEventId == Guid.Empty)
						if (oneMeet.CurrentGroupId == null || oneMeet.CurrentGroupId == currentGroup.GroupId)
							oneMeet.Events.Add(t);
				}
			}

			oneMeet.Events.Sort(CompareTimeSchedule);
		}

		#region Compare functions
		private static int CompareTimeSchedule(OneEvent k1, OneEvent k2)
		{
			int result = k1.TotalSeconds.CompareTo(k2.TotalSeconds);
			if (result == 0)
			{
				result = k1.EventSortKey.CompareTo(k2.EventSortKey);
				if (result == 0)
					result = k1.GroupSortKey.CompareTo(k2.GroupSortKey);
			}

			return result;
		}

		private static int CompareTimeSchedule(DispParticipant k1, DispParticipant k2)
		{
			int result = CompareTime(k1, k2);
			if (result == 0) result = CompareEventGroup(k1, k2);
			return result;
		}

		private static int CompareName(DispParticipant k1, DispParticipant k2)
		{
			int result = CompareFullName(k1, k2);
			if (result == 0) result = CompareEventGroup(k1, k2);
			return result;
		}

		private static int CompareGroupEvent(DispParticipant k1, DispParticipant k2)
		{
			int result = CompareGroup(k1, k2);
			if (result == 0) result = CompareEvent(k1, k2);
			if (result == 0) result = CompareFullName(k1, k2);
			return result;
		}

		private static int CompareEventGroup(DispParticipant k1, DispParticipant k2)
		{
			int result = CompareEvent(k1, k2);
			if (result == 0) result = CompareGroup(k1, k2);
			if (result == 0) result = CompareFullName(k1, k2);
			return result;
		}

		private static int CompareTime(DispParticipant k1, DispParticipant k2)
		{
			return k1.OneEvent.TotalSeconds.CompareTo(k2.OneEvent.TotalSeconds);
		}

		private static int CompareGroup(DispParticipant k1, DispParticipant k2)
		{
			return k1.OneEvent.GroupSortKey.CompareTo(k2.OneEvent.GroupSortKey);
		}

		private static int CompareEvent(DispParticipant k1, DispParticipant k2)
		{
			return k1.OneEvent.EventSortKey.CompareTo(k2.OneEvent.EventSortKey);
		}

		private static int CompareFullName(DispParticipant k1, DispParticipant k2)
		{
			return k1.Name.CompareTo(k2.Name);
		}
		#endregion
	}
}
