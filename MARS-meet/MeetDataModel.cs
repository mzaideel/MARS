using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MARS.Config;
using System.Data.Entity;
using System.Globalization;

namespace MARS.Meet
{
	//[DataObject]
    public partial class MARS_Meet_dbEntities : DbContext
	//public class MeetDataModel : MARS_Meet_dbEntities
	{
		//	public static Dictionary<string, string> AllowedXMarks = new Dictionary<string, string>()
		//			{
		//				{ "", "" },
		//				{ "S", "DNS" },
		//				{ "F", "DNF" },
		//				{ "Q", "DQ" },
		//				{ "W", "NQ" }
		//			};

		//	public static List<object> GetAllXMarks()
		//	{
		//		return (from n in AllowedXMarks
		//				select new
		//				{
		//					Key = n.Key,
		//					Value = n.Value
		//				}).ToList<object>();
		//	}

		//	#region Constructors
		//	private const string name = "MARS_Meet_dbEntities";

		//	public MeetDataModel() :
		//		this(String.Format("name={0}", name))
		//	{
		//	}

		//	public MeetDataModel(bool doLogging) :
		//		this(String.Format("name={0}", name), doLogging)
		//	{
		//	}

		//	public MeetDataModel(string connectionString)
		//		: this(connectionString, true)
		//	{
		//	}

		//	public MeetDataModel(string connectionString, bool doLogging)
		//		: base(EntityConnectionWrapperUtils.CreateEntityConnectionWithWrappers(connectionString, "EFTracingProvider"))
		//	{
		//		if (doLogging)
		//			startLogging();
		//	}
		//	#endregion

		//	#region IDisposable
		//	private bool isDisposed = false;

		//	protected override void Dispose(bool disposing)
		//	{
		//		if (!isDisposed)
		//		{
		//			if (disposing)
		//			{
		//				// free other managed objects that implement IDisposable only
		//				try { stopLogging(); }
		//				catch { }
		//			}

		//			isDisposed = true;
		//		}

		//		base.Dispose(disposing);
		//	}
		//	#endregion

		//	#region Logging
		//	private EntityLogging log = null;

		//	private void startLogging()
		//	{
		//		stopLogging();

		//		log = new EntityLogging() { Context = this };
		//		log.StartLogging(false);

		//		SavingChanges += MeetDataModel_SavingChanges;
		//	}

		//	private void stopLogging()
		//	{
		//		if (log != null)
		//		{
		//			SavingChanges -= MeetDataModel_SavingChanges;

		//			log.StopLogging(false);
		//			log = null;
		//		}
		//	}

		//	private void MeetDataModel_SavingChanges(object sender, EventArgs e)
		//	{
		//		LogManager.Save("<<<<<< MeetDataModel_SavingChanges >>>>>>");
		//	}
		//	#endregion

		//	#region Get
		public mars_MMeet GetMeet(Guid meetId)
		{
			mars_MMeet meet = (from m in mars_MMeet where m.MeetId == meetId select m).SingleOrDefault();
			return meet;
		}

		//	public List<mars_MEventEntries> GetAllEvents(Guid meetId)
		//	{
		//		return (from e in mars_MEventEntries
		//				where e.MeetId == meetId
		//				orderby e.SortKey
		//				select e
		//				).ToList<mars_MEventEntries>();
		//	}

		//	public List<mars_MEventEntries> GetAllEvents(Guid meetId, string groupId)
		//	{
		//		return (from g in mars_MGroups
		//				join e in mars_MEventEntries on new { g.MeetId, g.GroupId } equals new { e.MeetId, e.GroupId }
		//				where g.MeetId == meetId && g.GroupId == groupId
		//				orderby e.SortKey
		//				select e).ToList<mars_MEventEntries>();
		//	}

		//	//		case 1:
		//	//			return (from e in mars_MEventEntries
		//	//					join g in mars_MGroups on new { e.MeetId, e.GroupId } equals new { g.MeetId, g.GroupId }
		//	//					where e.MeetId == meetId && e.HeatGroupNo == 0
		//	//					orderby g.SortKey, e.SortKey, e.RoundNo
		//	//					select new { EventEntry = e, Group = g }
		//	//					).ToList<object>();

		//	//		case 2:
		//	//			return (from e in mars_MEventEntries
		//	//					join g in mars_MGroups on new { e.MeetId, e.GroupId } equals new { g.MeetId, g.GroupId }
		//	//					where e.MeetId == meetId && e.HeatGroupNo == 0
		//	//					orderby e.DayNo, e.Seconds, e.SortKey, e.RoundNo, g.SortKey
		//	//					select new { Group = g, EventEntry = e }
		//	//					).ToList<object>();

		//	//		default:
		//	//			return null;

		//	public List<object> GetUniqueEvents(Guid meetId)
		//	{
		//		return (from f in mars_MEventEntries
		//				where f.MeetId == meetId
		//				group f by new { id = f.SortKeyEventId, val = f.EventId } into g
		//				select new { SortKey = g.Key.id, EventId = g.Key.val }).OrderBy(n => n.SortKey).Select(n => new { EventId = n.EventId }).ToList<object>();
		//	}

		//	public List<object> GetTimetableEvents(Guid meetId, int dayNo = 1)
		//	{
		//		return (from e in mars_MEventEntries
		//				join g in mars_MGroups on new { e.MeetId, e.GroupId } equals new { g.MeetId, g.GroupId }
		//				where e.MeetId == meetId && e.DayNo == dayNo && e.HeatGroupNo == 0 && e.EventType == 1
		//				orderby e.DayNo, e.Seconds, e.SortKey, e.RoundNo, g.SortKey
		//				select new { EventEntry = e, Group = g }
		//				).ToList<object>();
		//	}

		//	public List<object> GetParticipantsInMeet(Guid meetId)
		//	{
		//		return (from t in mars_MTeams
		//				join p in mars_MParticipants on new { t.MeetId, t.TeamId } equals new { p.MeetId, p.TeamId }
		//				join m in mars_MMarks on new { p.MeetId, p.ParticipantId } equals new { m.MeetId, m.ParticipantId }
		//				join e in mars_MEventEntries on new { m.MeetId, m.EventEntryId } equals new { e.MeetId, e.EventEntryId }
		//				join g in mars_MGroups on new { e.MeetId, e.GroupId } equals new { g.MeetId, g.GroupId }
		//				where t.MeetId == meetId
		//				orderby t.SortKey, p.FullName, g.SortKey, e.SortKey
		//				select new { Team = t, Participant = p, Mark = m, EventEntry = e, Group = g }
		//			   ).ToList<object>();
		//	}

		//	public List<object> GetParticipantsInTeam(Guid meetId, Guid teamId)
		//	{
		//		return (from t in mars_MTeams
		//				join p in mars_MParticipants on new { t.MeetId, t.TeamId } equals new { p.MeetId, p.TeamId }
		//				join m in mars_MMarks on new { p.MeetId, p.ParticipantId } equals new { m.MeetId, m.ParticipantId }
		//				join e in mars_MEventEntries on new { m.MeetId, m.EventEntryId } equals new { e.MeetId, e.EventEntryId }
		//				join g in mars_MGroups on new { e.MeetId, e.GroupId } equals new { g.MeetId, g.GroupId }
		//				where t.MeetId == meetId && t.TeamId == teamId
		//				orderby t.SortKey, p.FullName, g.SortKey, e.SortKey
		//				select new { Team = t, Participant = p, Mark = m, EventEntry = e, Group = g }
		//			   ).ToList<object>();
		//	}

		//	public List<object> GetParticipantsInGroup(Guid meetId, string groupId)
		//	{
		//		return (from g in mars_MGroups
		//				join e in mars_MEventEntries on new { g.MeetId, g.GroupId } equals new { e.MeetId, e.GroupId }
		//				join m in mars_MMarks on new { e.MeetId, e.EventEntryId } equals new { m.MeetId, m.EventEntryId }
		//				join p in mars_MParticipants on new { m.MeetId, m.ParticipantId } equals new { p.MeetId, p.ParticipantId }
		//				join t in mars_MTeams on new { p.MeetId, p.TeamId } equals new { t.MeetId, t.TeamId }
		//				where g.MeetId == meetId && g.GroupId == groupId
		//				orderby g.SortKey, e.SortKey, e.RoundNo, m.MarkCode, e.HeatGroupNo, m.LaneNo, t.SortKey, p.FullName
		//				select new { Team = t, Participant = p, Mark = m, EventEntry = e, Group = g }
		//			   ).ToList<object>();
		//	}

		//	public List<object> GetParticipantsInEvent(Guid meetId, string eventId)
		//	{
		//		return (from e in mars_MEventEntries
		//				join g in mars_MGroups on new { e.MeetId, e.GroupId } equals new { g.MeetId, g.GroupId }
		//				join m in mars_MMarks on new { e.MeetId, e.EventEntryId } equals new { m.MeetId, m.EventEntryId }
		//				join p in mars_MParticipants on new { m.MeetId, m.ParticipantId } equals new { p.MeetId, p.ParticipantId }
		//				join t in mars_MTeams on new { p.MeetId, p.TeamId } equals new { t.MeetId, t.TeamId }
		//				where e.MeetId == meetId && e.EventId == eventId
		//				orderby g.SortKey, e.SortKey, e.RoundNo, m.MarkCode, e.HeatGroupNo, m.LaneNo, t.SortKey, p.FullName
		//				select new { Team = t, Participant = p, Mark = m, EventEntry = e, Group = g }
		//			   ).ToList<object>();
		//	}

		//	public List<object> GetParticipantsInOneEvent(Guid meetId, Guid eventEntryId)
		//	{
		//		return (from g in mars_MGroups
		//				join e in mars_MEventEntries on new { g.MeetId, g.GroupId } equals new { e.MeetId, e.GroupId }
		//				join m in mars_MMarks on new { e.MeetId, e.EventEntryId } equals new { m.MeetId, m.EventEntryId }
		//				join p in mars_MParticipants on new { m.MeetId, m.ParticipantId } equals new { p.MeetId, p.ParticipantId }
		//				join t in mars_MTeams on new { p.MeetId, p.TeamId } equals new { t.MeetId, t.TeamId }
		//				where e.MeetId == meetId && e.EventEntryId == eventEntryId
		//				orderby g.SortKey, e.SortKey, e.RoundNo, m.MarkCode, e.HeatGroupNo, m.LaneNo, t.SortKey, p.FullName
		//				select new { Team = t, Participant = p, Mark = m, EventEntry = e, Group = g }
		//			   ).ToList<object>();
		//	}

		//	public List<object> GetMedalCompetition(Guid meetId)
		//	{
		//		return (from e in mars_MEventEntries
		//				join m in mars_MMarks on e.EventEntryId equals m.EventEntryId
		//				join p in mars_MParticipants on m.ParticipantId equals p.ParticipantId
		//				join t in mars_MTeams on p.TeamId equals t.TeamId
		//				where e.MeetId == meetId && e.RoundNo == e.NoOfRounds && (m.Pos == 1 || m.Pos == 2 || m.Pos == 3)
		//				group m by t.ShortName into g
		//				let P1 = g.Count(x => x.Pos == 1)
		//				let P2 = g.Count(x => x.Pos == 2)
		//				let P3 = g.Count(x => x.Pos == 3)
		//				let P4 = P1 + P2 + P3
		//				let P5 = g.Key
		//				orderby P1 descending, P2 descending, P3 descending, P4 descending, P5
		//				select new
		//				{
		//					Pos1 = P1,
		//					Pos2 = P2,
		//					Pos3 = P3,
		//					Count = P4,
		//					Team = P5
		//				}
		//			   ).ToList<object>();
		//	}

		//	public List<object> GetPlacingCompetition(Guid meetId)
		//	{
		//		return (from e in mars_MEventEntries
		//				join m in mars_MMarks on e.EventEntryId equals m.EventEntryId
		//				join p in mars_MParticipants on m.ParticipantId equals p.ParticipantId
		//				join t in mars_MTeams on p.TeamId equals t.TeamId
		//				where e.MeetId == meetId && e.RoundNo == e.NoOfRounds && (m.Pos >= 1 &&  m.Pos <= 8)
		//				group m by t.ShortName into g
		//				let P1 = g.Count(x => x.Pos == 1)
		//				let P2 = g.Count(x => x.Pos == 2)
		//				let P3 = g.Count(x => x.Pos == 3)
		//				let P4 = g.Count(x => x.Pos == 4)
		//				let P5 = g.Count(x => x.Pos == 5)
		//				let P6 = g.Count(x => x.Pos == 6)
		//				let P7 = g.Count(x => x.Pos == 7)
		//				let P8 = g.Count(x => x.Pos == 8)
		//				let P9 = P1 * 8 + P2 * 7 + P3 * 6 + P4 * 5 + P5 * 4 + P6 * 3 + P7 * 2 + P8 * 1
		//				let P10 = g.Key
		//				orderby P9 descending, P1 descending, P2 descending, P3 descending, P4 descending, P5 descending, P6 descending, P7 descending, P8 descending, P10
		//				select new
		//				{
		//					Pos1 = P1,
		//					Pos2 = P2,
		//					Pos3 = P3,
		//					Pos4 = P4,
		//					Pos5 = P5,
		//					Pos6 = P6,
		//					Pos7 = P7,
		//					Pos8 = P8,
		//					Points = P9,
		//					Team = P10
		//				}
		//			   ).ToList<object>();
		//	}
		//	#endregion

		//	#region Updating
		//	public static void UpdateParticipant(Guid meetId, List<string> values)
		//	{
		//		using (MeetDataModel db = new MeetDataModel())
		//		{
		//			//  0         1                2               3               4                  5              6              7             8          9              10              11                     12
		//			//                                             P               P                  E              E              E             E          E              M               M                      P
		//			// "MarkId", "ParticipantId", "EventEntryId", "FullNameEdit", "YearOfBirthEdit", "GroupIdEdit", "EventIdEdit", "SubEventId", "RoundNo", "HeatGroupNo", "MarkCodeEdit", "QualifyingEntryEdit", "BibNo"

		//			Guid participantId = new Guid(values[1]);
		//			mars_MParticipants q1 = (from p in db.mars_MParticipants where p.MeetId == meetId && p.ParticipantId == participantId select p).SingleOrDefault();

		//			if (q1.FullName != values[3]) q1.FullName = values[3];
		//			int year = values[4].ToOrOther<int>(0);
		//			if (q1.YearOfBirth != year) q1.YearOfBirth = year;
		//			int bibNo = values[12].ToOrOther<int>(0);
		//			if (q1.BibNo != bibNo) q1.BibNo = bibNo;
		//			db.SaveChanges();

		//			Guid markId = new Guid(values[0]);
		//			mars_MMarks q2 = (from m in db.mars_MMarks where m.MeetId == meetId && m.MarkId == markId select m).SingleOrDefault();
		//			if (q2.MarkCode != values[10]) q2.MarkCode = values[10];
		//			if (q2.QualifyingEntry != values[11]) q2.QualifyingEntry = values[11];

		//			Guid eventEntryId = q2.EventEntryId;
		//			mars_MEventEntries q3 = (from e in db.mars_MEventEntries where e.MeetId == meetId && e.EventEntryId == eventEntryId select e).SingleOrDefault();

		//			string groupId = values[5];
		//			string eventId = values[6];
		//			string subEventId = values[7];
		//			byte roundNo = values[8].ToOrOther<byte>(1);
		//			byte heatGroupNo = values[9].ToOrOther<byte>(0);

		//			if (q3.GroupId != groupId || q3.EventId != eventId || /*q3.SubEventId != subEventId ||*/ q3.RoundNo != roundNo || q3.HeatGroupNo != heatGroupNo)
		//			{
		//				mars_MEventEntries q4 = (from e in db.mars_MEventEntries
		//										 where e.MeetId == meetId && e.GroupId == groupId && e.EventId == eventId && /*e.SubEventId == subEventId &&*/ e.RoundNo == roundNo && e.HeatGroupNo == heatGroupNo
		//										 select e).SingleOrDefault();
		//				if (q4 != null)
		//					q2.EventEntryId = q4.EventEntryId;
		//			}

		//			db.SaveChanges();
		//		}
		//	}

		//	public static void InsertMark(Guid meetId, List<string> values)
		//	{
		//		using (MeetDataModel db = new MeetDataModel())
		//		{
		//			Guid participantId = new Guid(values[1]);
		//			mars_MParticipants q1 = (from p in db.mars_MParticipants where p.MeetId == meetId && p.ParticipantId == participantId select p).SingleOrDefault();

		//			string groupId = values[5];
		//			string eventId = values[6];
		//			string subEventId = values[7];
		//			byte roundNo = values[8].ToOrOther<byte>(1);
		//			byte heatGroupNo = values[9].ToOrOther<byte>(0);

		//			mars_MEventEntries q2 = (from e in db.mars_MEventEntries
		//									 where e.MeetId == meetId && e.GroupId == groupId && e.EventId == eventId && /*e.SubEventId == subEventId &&*/ e.RoundNo == roundNo && e.HeatGroupNo == heatGroupNo
		//									 select e).SingleOrDefault();

		//			if (q2 != null)
		//			{
		//				mars_MMarks m = new mars_MMarks();
		//				m.MarkCode = values[10];
		//				m.QualifyingEntry = values[11];
		//				m.EventEntryId = q2.EventEntryId;

		//				q1.Marks.Add(m);
		//				db.SaveChanges();
		//			}
		//		}
		//	}

		//	public static void DeleteMark(Guid meetId, List<Guid> markIds)
		//	{
		//		using (MeetDataModel db = new MeetDataModel())
		//		{
		//			foreach (Guid markId in markIds)
		//			{
		//				mars_MMarks mark = (from m in db.mars_MMarks where m.MarkId == markId select m).SingleOrDefault();
		//				if (mark != null)
		//					db.DeleteObject(mark);
		//			}

		//			db.SaveChanges();
		//		}
		//	}

		//	public static void UpdateMarkCode(Guid meetId, List<Guid> markIds, string markCode)
		//	{
		//		using (MeetDataModel db = new MeetDataModel())
		//		{
		//			foreach (Guid markId in markIds)
		//			{
		//				mars_MMarks mark = (from m in db.mars_MMarks where m.MarkId == markId select m).SingleOrDefault();
		//				if (mark != null)
		//					mark.MarkCode = markCode;
		//			}

		//			db.SaveChanges();
		//		}
		//	}

		//	public static void GenerateBibNo(Guid meetId)
		//	{
		//		using (MeetDataModel db = new MeetDataModel())
		//		{
		//			var query = from p in db.mars_MParticipants
		//						join t in db.mars_MTeams on p.TeamId equals t.TeamId
		//						where p.MeetId == meetId
		//						orderby t.SortKey, p.FullName
		//						select p;

		//			int bibNo = 0;
		//			foreach (mars_MParticipants participant in query)
		//				participant.BibNo = ++bibNo;

		//			db.SaveChanges();
		//		}
		//	}
		//	#endregion
		//}

		//public partial class mars_MGroups : EntityObject, IEntityProcessSaving
		//{
		//	#region IEntityProcessSaving implementation
		//	public void Delete() { }

		//	public void AddStepOne() { }

		//	public void AddStepTwo()
		//	{
		//		if (Meet != null)
		//			MeetId = Meet.MeetId;
		//	}

		//	public void Validate(EntityState state) { }

		//	public void FieldChanged(string name, object orgValue, object currentValue) { }
		//	public void FieldAdded(string name, object currentValue) { }
		//	public void FieldDeleted(string name, object orgValue) { }
		//	#endregion

		//	public static mars_MGroups Create(mars_CGroups from)
		//	{
		//		return new mars_MGroups()
		//		{
		//			GroupId = from.GroupId,
		//			Age = from.Age,
		//			Gender = from.Gender,
		//			SortKey = from.SortKey,
		//			Text = from.Text
		//		};
		//	}
	}

	public partial class mars_MMeet
	{
		public string Header
		{
			get
			{
				StringBuilder sb = new StringBuilder();

				sb.AppendFormat("{0} • ", Text);

				CultureInfo en = new CultureInfo("en-US");
				if (Days > 1)
					sb.Append(String.Format("{0} - {1}", StartDate.ToString("yyyy MMM dd", en), StartDate.AddDays((double)(Days - 1)).ToString("MMM dd", en)));
				else
					sb.Append(String.Format("{0}", StartDate.ToString("yyyy MMM dd", en)));

				sb.AppendFormat(" • {0} • {1}", Stadium, Location);

				return sb.ToString();
			}
		}
	}

	public partial class mars_MEventEntries
	{
		//public static mars_MEventEntries Create(mars_CSubEvents from)
		//{
		//	return new mars_MEventEntries()
		//	{
		//		EventId = from.mars_CEvents.EventId,
		//		Text = from.mars_CEvents.Text,

		//		SubEventId = from.SubEventId,
		//		SubText = from.Text,

		//		SortKey = from.SortKey,
		//		SortKeyEventId = from.mars_CEvents.SortKey,

		//		EventType = from.mars_CEvents.EventType,
		//		ResultPrecision = from.mars_CEvents.ResultPrecision,
		//		ResultLayout = from.mars_CEvents.ResultLayout,
		//		ManualTimeAdjustment = from.mars_CEvents.ManualTimeAdjustment,
		//		WindMeasure = from.mars_CEvents.WindMeasure,
		//		WindMaxAllowed = from.mars_CEvents.WindMaxAllowed
		//	};
		//}

		public static mars_MEventEntries Create(mars_MEventEntries from)
		{
			return new mars_MEventEntries
			{
				EventEntryId = Guid.NewGuid(),
				MeetId = from.MeetId,
				GroupId = from.GroupId,
				EventId = from.EventId,
				SubEventId = from.SubEventId,
				Text = from.Text,
				SubText = from.SubText,
				Indoor = from.Indoor,
				Outdoor = from.Outdoor,
				DayNo = from.DayNo,
				Seconds = from.Seconds,
				SortKey = from.SortKey,
				SortKeyEventId = from.SortKeyEventId,
				EventType = from.EventType,
				ResultPrecision = from.ResultPrecision,
				ResultLayout = from.ResultLayout,
				ManualTimeAdjustment = from.ManualTimeAdjustment,
				WindMeasure = from.WindMeasure,
				WindMaxAllowed = from.WindMaxAllowed,
				RoundNo = from.RoundNo,
				HeatGroupNo = from.HeatGroupNo,
				UserDefined = from.UserDefined,
				Started = from.Started,
				Stopped = from.Stopped,
				LiveResults = from.LiveResults
			};
		}

		//public string Time
		//{
		//	get
		//	{
		//		if (Seconds <= 0)
		//			return "";

		//		TimeSpan t = TimeSpan.FromSeconds((double)Seconds);
		//		return string.Format("{0:D2}:{1:D2}", t.Hours, t.Minutes);
		//	}
		//	set
		//	{ 
		//	}
		//}

		public void SetDateTime(DateTime d, DateTime meetStartDate)
		{
			byte dayNo = (byte)(d.Date.Subtract(meetStartDate.Date).TotalDays + 1);
			int seconds = d.Hour * 3600 + d.Minute * 60 + d.Second;

			if (dayNo != DayNo)
				DayNo = dayNo;

			if (seconds != Seconds)
				Seconds = seconds;
		}

		//public string StateText
		//{
		//	get 
		//	{
		//		switch (State ?? "N")
		//		{ 
		//			case "O":
		//				return "On going";

		//			case "F":
		//				return "Finished";

		//			default:
		//				return "Not yet started";
		//		}
		//	}
		//	set
		//	{ }
		//}

		//	public string RHGShort
		//	{
		//		get
		//		{
		//			StringBuilder sb = new StringBuilder();
		//			sb.Append(RoundShorthand);
		//			if (HeatGroupNo > 0) sb.Append(String.Format("-{0}", HeatGroupNo));
		//			return sb.ToString();
		//		}

		//	}

		//	public string RHGLong
		//	{
		//		get
		//		{
		//			StringBuilder sb = new StringBuilder();
		//			sb.Append(RoundText);
		//			if (HeatGroupNo > 0) sb.Append(String.Format(" - {0} {1}", CEventHelper.IsRunningEvent(EventType) ? "heat" : "gruppe", HeatGroupNo));
		//			return sb.ToString();
		//		}
		//	}

		//	public string EventTextShort
		//	{
		//		get
		//		{
		//			StringBuilder sb = new StringBuilder();
		//			sb.Append(EventId);
		//			if (SubEventId.IsNotNull())
		//				sb.Append(String.Format(" / {0}", SubEventId));
		//			return sb.ToString();
		//		}
		//	}

		//	public string EventTextLong
		//	{
		//		get
		//		{
		//			StringBuilder sb = new StringBuilder();
		//			sb.Append(Text);
		//			if (SubEventId.IsNotNull())
		//				sb.Append(String.Format(" / {0}", SubText));

		//			return sb.ToString();
		//		}
		//	}
	}

	//public partial class mars_MTeams : EntityObject, IEntityProcessSaving
	//{
	//	public static mars_MTeams Create(mars_UTeams from)
	//	{
	//		return new mars_MTeams()
	//		{
	//			FullName = from.FullName,
	//			ShortName = from.ShortName,
	//			Name3 = from.Name3,
	//			Name6 = from.Name6,
	//			Name9 = from.Name9,
	//			DAFName = from.DAFName,
	//			Nation = from.Nation,
	//			SortKey = from.SortKey
	//		};
	//	}
	//}

	//public partial class mars_MParticipants : EntityObject, IEntityProcessSaving
	//{
	//}

	//public partial class mars_MMarks : EntityObject, IEntityProcessSaving
	//{
	//	public string MarkCodeText
	//	{
	//		get
	//		{
	//			return MarkCode.IsNull() ? "" : MeetDataModel.AllowedXMarks[MarkCode];
	//		}
	//	}

	//	public string MarkOutput
	//	{
	//		get
	//		{
	//			return MarkCode.IsNull() ? MarkText : MarkCodeText;
	//		}
	//	}

	//	public string PosOutput
	//	{
	//		get
	//		{
	//			return (Pos == null || Pos == 0) ? "" : Pos.ToString();
	//		}
	//	}

	//	public string LaneShort
	//	{
	//		get
	//		{
	//			StringBuilder sb = new StringBuilder();
	//			if (LaneNo > 0) sb.Append(String.Format("{0}", LaneNo));
	//			return sb.ToString();
	//		}
	//	}

	//	//public string GetLaneLong(CEventType eventType)
	//	//{
	//	//		StringBuilder sb = new StringBuilder();
	//	//		if (LaneNo > 0) sb.Append(String.Format("{0} {1}", CEventHelper.IsRunningEvent(eventType) ? "bane" : "rank", LaneNo));
	//	//		return sb.ToString();
	//	//}
	//}
}
