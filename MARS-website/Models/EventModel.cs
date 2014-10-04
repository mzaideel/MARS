using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Mars2.Data;
using MARS.Utilities;
using MARS.Meet;

namespace MARS.Web.Models
{
	public class MeetModel
	{
		public List<OneMeet> MeetList { get; set; }

		public static Dictionary<string, string> AllowedXMarks = new Dictionary<string, string>()
				{
					{ "", "" },
					{ "S", "DNS" },
					{ "F", "DNF" },
					{ "Q", "DQ" },
					{ "W", "NQ" }
				};
	}

	public enum Selection { Events, OneEvent, Teams, OneTeam };

	public class OneMeet
	{
		public Guid MeetId { get; set; }
		public string Description { get; set; }

		public Selection Selection { get; set; }

		public string CurrentGroupId { get; set; }
		public Guid CurrentEventId { get; set; }
		public int CurrentDay { get; set; }
		public int CurrentSortBy { get; set; }
		public int NoOfDays { get; set; }

		public List<OneGroup> Groups { get; set; }
		public List<OneEvent> Events { get; set; }

		public List<OneTeam> Teams { get; set; }
		public List<List<OneTeam>> TeamRows { get; set; }
		public OneTeam CurrentTeam { get; set; }

		public MedalCompetition MedalCompetition { get; set; }
		public PointsCompetition PointsCompetition { get; set; }
	}

	public class OneTeam
	{
		public Guid Id { get; set; }
		public string Name { get; set; }

		public List<DispParticipant> Participants { get; set; }
	}

	public class OneGroup
	{
		public string GroupId { get; set; }
		public string GroupText { get; set; }
		public int SortKey { get; set; }

		public List<OneEvent> Events { get; set; }
	}

	public class OneEvent
	{
		public Guid Id { get; set; }
		public string GroupId { get; set; }
		public string EventId { get; set; }
		public int RoundNo { get; set; }
		public int NoOfRounds { get; set; }
		public int Day { get; set; }
		public int Seconds { get; set; }
		public long TotalSeconds
		{
			get
			{
				return Day * 24 * 60 * 60 + Seconds;
			}
		}

		public string Time
		{
			get
			{
				if (Seconds <= 0)
					return "";

				TimeSpan t = TimeSpan.FromSeconds((double)Seconds);
				return string.Format("{0:D2}:{1:D2}", t.Hours, t.Minutes);
			}
		}

		public string DayTime
		{
			get
			{
				return String.Format("{0} - {1}", Day, Time);
			}
		}

		public string GroupText { get; set; }
		public string EventText { get; set; }
		public string RoundText { get; set; }

		public string EventText2
		{
			get
			{
				string s = EventText;
				if (RoundNo < NoOfRounds || NoOfRounds > 1)
					s = s + " - " + RoundText;
				return s;
			}
		}

		public string State { get; set; }
		public string StateText
		{
			get
			{
				switch (State ?? "N")
				{
					case "O":
						return "On going";

					case "F":
						return "Finished";

					default:
						return "Not yet started";
				}
			}
		}

		public int GroupSortKey { get; set; }
		public int EventSortKey { get; set; }

		public string Data { get; set; }

		public Staevne Staevne { get; set; }
		public StartList StartList { get; set; }
		public ResultList ResultList { get; set; }
		public RecordList RecordList { get; set; }

		public void BuildRecordList()
		{
			if (Staevne != null && Staevne.Ovelser != null && Staevne.Ovelser.Length == 1 && Staevne.Ovelser[0].Rekorder != null && Staevne.Ovelser[0].Rekorder.Length > 0)
			{
				RecordList = new RecordList();
				RecordList.Ov = Staevne.Ovelser[0];
				RecordList.Lines = new List<DispRecord>();

				string prevID = "";
				foreach (Rekord rekord in Staevne.Ovelser[0].Rekorder)
				{
					DispRecord r = new DispRecord() { Name = rekord.FormatRekordTekst, Result = rekord.Resultat, ID = "" };
					if (rekord.Id != prevID)
					{
						r.ID = rekord.Id;
						prevID = r.ID;
					}
					RecordList.Lines.Add(r);
				}
			}
		}

		public void BuildStartList(List<mars_MTeams> teamList)
		{
			if (Staevne != null && Staevne.Ovelser != null && Staevne.Ovelser.Length == 1)
			{
				Ovelse ov = Staevne.Ovelser[0];
				if (ov.Heats != null)
				{
					bool seedet = ov.FindesSeedning;

					StartList = new StartList();
					StartList.Ov = ov;
					StartList.Heats = new List<DispHeat>();

					foreach (Heat heat in StartList.Ov.Heats)
					{
						DispHeat h = new DispHeat() { Participants = new List<DispParticipant>() };

						if (heat.Nr > 0 && StartList.Ov.AntalHeat > 1)
							h.Header = String.Format("HEAT {0} / {1}", heat.Nr, StartList.Ov.AntalHeat);
						else if (heat.Id.IsNotNull() && heat.Id != "0" && StartList.Ov.AntalHeat > 1)
							h.Header = String.Format("HEAT {0}", heat.Id);
						else if (heat.Nr == 0 && seedet)
							h.Header = "SCRATCH";
						else
							h.Header = "";

						if (heat.Deltagere != null)
						{
							if (seedet)
								Array.Sort(heat.Deltagere, (k1, k2) => k1.CompareStartListOrder(k2));

							int orderNo = 0;
							foreach (ResDeltager deltager in heat.Deltagere)
							{
								DispParticipant d = new DispParticipant();

								mars_MTeams key = teamList.FirstOrDefault(t => deltager.Klub == t.ShortName);
								d.TeamId = key != null ? key.TeamId : Guid.Empty;

								orderNo++;

								d.OrderNo = deltager.BaneNr > 0 ? deltager.BaneNr.ToString() : orderNo.ToString();
								d.BibNo = StartList.Ov.StafetOvelse ? "" : deltager.StartNr.ToString();

								if (StartList.Ov.StafetOvelse)
								{
									StringBuilder sb = new StringBuilder();

									if (deltager.StafetDeltagere != null && deltager.StafetDeltagere.Length > 0)
									{
										foreach (var stafetDeltager in deltager.StafetDeltagere)
										{
											if (sb.Length > 0) sb.Append(", ");
											if (stafetDeltager.Deltager != null)
												sb.Append(stafetDeltager.Deltager.EfternavnFornavn);
											else
												sb.Append(stafetDeltager.StartNr.ToString());
										}
									}

									d.Name = sb.ToString();

									d.YOB = "";
									d.Team = deltager.Navn;
								}
								else
								{
									d.Name = deltager.EfternavnFornavn;
									d.YOB = deltager.Aargang;
									d.Team = deltager.Klub;
								}

								d.SB = deltager.Seedning == "X" ? "" : deltager.Seedning;
								d.PB = deltager.Pr == "X" ? "" : deltager.Pr;
								if (!deltager.Startet)
									d.Comment = deltager.ResKodeText;

								h.Participants.Add(d);
							}
						}

						StartList.Heats.Add(h);
					}
				}
			}
		}

		private void doBuildSerie(Ovelse ov, ResDeltager deltager, DispParticipant d)
		{
			if (ov.KasteOvelse && deltager.KastSerier != null && deltager.KastSerier.Length > 0)
			{
				d.Serie1 = new List<string>();

				foreach (Kast kast in deltager.KastSerier)
					d.Serie1.Add(kast.Resultat ?? "");

				if (d.Serie1.Count > ResultList.NoInSerie)
					ResultList.NoInSerie = d.Serie1.Count;
			}

			if (ov.HorizontalSpringOvelse && deltager.SpringHSerier != null && deltager.SpringHSerier.Length > 0)
			{
				d.Serie1 = new List<string>();

				foreach (SpringHorizontal spring in deltager.SpringHSerier)
				{
					string s = spring.Resultat ?? "";
					if (s != "" && ov.VindPrDeltager)
					{
						if (spring.Vind != null && spring.Vind != "")
							s = s + " (" + spring.Vind + ")";
					}
					d.Serie1.Add(s);
				}

				if (d.Serie1.Count > ResultList.NoInSerie)
					ResultList.NoInSerie = d.Serie1.Count;
			}

			if (ov.VertikalSpringOvelse && deltager.SpringVSerier != null && deltager.SpringVSerier.Count > 0)
			{
				d.Serie1 = new List<string>();
				for (int i = 0; i < ov.SpringHojder.Count; i++)
					d.Serie1.Add("");

				foreach (SpringVertikal spring in deltager.SpringVSerier)
				{
					if (spring.OmspringNr == 0)
					{
						int n = ov.SpringHojder.IndexOf(spring.ResultatValue);
						if (n >= 0 && n < d.Serie1.Count)
							d.Serie1[n] = spring.Spring ?? "";
					}
				}
			}
		}

		private void doBuildOngoingResultList(Ovelse ov, List<mars_MTeams> teamList)
		{
			ResultList = new ResultList();
			ResultList.Ov = ov;
			ResultList.Heats = new List<DispHeat>();
			ResultList.NoInSerie = 0;

			if (ov.VertikalSpringOvelse && ov.SpringHojder.Count > 0)
			{
				ResultList.JumpSchema = new List<string>();
				for (int i = 0; i < ov.SpringHojder.Count; i++)
					ResultList.JumpSchema.Add(ov.SpringHojder[i].ToString());
			}

			foreach (Heat heat in ResultList.Ov.Heats)
			{
				DispHeat h = new DispHeat() { Participants = new List<DispParticipant>() };

				StringBuilder sb = new StringBuilder();
				if (heat.Nr > 0 && StartList.Ov.AntalHeat > 1)
					sb.AppendFormat("GROUP {0} / {1}", heat.Nr, ResultList.Ov.AntalHeat);

				if (ov.VindPrHeat)
				{
					if (sb.Length > 0)
						sb.Append("  -  ");
					sb.AppendFormat("Wind: {0}", heat.Vind);
				}

				h.Header = sb.Length > 0 ? sb.ToString() : "";

				if (heat.Deltagere != null)
				{
					Array.Sort(heat.Deltagere, (k1, k2) => k1.CompareResultListHeatOrder(k2));

					foreach (ResDeltager deltager in heat.Deltagere)
					{
						DispParticipant d = new DispParticipant();

						mars_MTeams key = teamList.FirstOrDefault(t => deltager.Klub == t.ShortName);
						d.TeamId = key != null ? key.TeamId : Guid.Empty;

						d.OrderNo = deltager.BaneNr.ToString();
						d.BibNo = StartList.Ov.StafetOvelse ? "" : deltager.StartNr.ToString();

						if (StartList.Ov.StafetOvelse)
						{
							sb = new StringBuilder();

							if (deltager.StafetDeltagere != null && deltager.StafetDeltagere.Length > 0)
							{
								foreach (var stafetDeltager in deltager.StafetDeltagere)
								{
									if (sb.Length > 0) sb.Append(", ");
									if (stafetDeltager.Deltager != null)
										sb.Append(stafetDeltager.Deltager.EfternavnFornavn);
									else
										sb.Append(stafetDeltager.StartNr.ToString());
								}
							}

							d.Name = sb.ToString();

							d.YOB = "";
							d.Team = deltager.Navn;
						}
						else
						{
							d.Name = deltager.EfternavnFornavn;
							d.YOB = deltager.Aargang;
							d.Team = deltager.Klub;
						}

						d.SB = deltager.Seedning == "X" ? "" : deltager.Seedning;
						d.PB = deltager.Pr == "X" ? "" : deltager.Pr;
						d.Comment = deltager.Kommentar;

						//if (d.Comment.IsNull())
						//{
						//	if (deltager.NewPB)
						//		d.Comment = "PB";
						//	else if (deltager.NewSB)
						//		d.Comment = "SB";
						//}

						if (deltager.ResKodeText == "")
						{
							d.Position = deltager.Plac.ToString();
							d.Result = deltager.Resultat;
						}
						else
						{
							d.Position = "";
							d.Result = deltager.ResKodeText;
						}

						doBuildSerie(ov, deltager, d);

						h.Participants.Add(d);
					}
				}

				ResultList.Heats.Add(h);
			}
		}

		private void doBuildResultList(Ovelse ov, List<mars_MTeams> teamList)
		{
			ResultList = new ResultList();
			ResultList.Ov = ov;
			ResultList.Heats = new List<DispHeat>();
			ResultList.NoInSerie = 0;

			if (ov.VertikalSpringOvelse && ov.SpringHojder.Count > 0)
			{
				ResultList.JumpSchema = new List<string>();
				for (int i = 0; i < ov.SpringHojder.Count; i++)
					ResultList.JumpSchema.Add(ov.SpringHojder[i].ToString());
			}

			foreach (Heat heat in ResultList.Ov.Heats)
			{
				if (heat.Nr == 0)
					continue;

				DispHeat h = new DispHeat() { Participants = new List<DispParticipant>() };

				StringBuilder sb = new StringBuilder();
				if (heat.Nr > 0 && StartList.Ov.AntalHeat > 1)
					sb.AppendFormat("HEAT {0} / {1}", heat.Nr, ResultList.Ov.AntalHeat);

				if (ov.VindPrHeat)
				{
					if (sb.Length > 0)
						sb.Append("  -  ");
					sb.AppendFormat("Wind: {0}", heat.Vind);
				}

				h.Header = sb.Length > 0 ? sb.ToString() : "";

				if (heat.Deltagere != null)
				{
					Array.Sort(heat.Deltagere, (k1, k2) => k1.CompareResultListHeatOrder(k2));

					foreach (ResDeltager deltager in heat.Deltagere)
					{
						DispParticipant d = new DispParticipant();

						mars_MTeams key = teamList.FirstOrDefault(t => deltager.Klub == t.ShortName);
						d.TeamId = key != null ? key.TeamId : Guid.Empty;

						d.OrderNo = deltager.BaneNr.ToString();
						d.BibNo = StartList.Ov.StafetOvelse ? "" : deltager.StartNr.ToString();

						if (StartList.Ov.StafetOvelse)
						{
							sb = new StringBuilder();

							if (deltager.StafetDeltagere != null && deltager.StafetDeltagere.Length > 0)
							{
								foreach (var stafetDeltager in deltager.StafetDeltagere)
								{
									if (sb.Length > 0) sb.Append(", ");
									if (stafetDeltager.Deltager != null)
										sb.Append(stafetDeltager.Deltager.EfternavnFornavn);
									else
										sb.Append(stafetDeltager.StartNr.ToString());
								}
							}

							d.Name = sb.ToString();

							d.YOB = "";
							d.Team = deltager.Navn;
						}
						else
						{
							d.Name = deltager.EfternavnFornavn;
							d.YOB = deltager.Aargang;
							d.Team = deltager.Klub;
						}

						d.SB = deltager.Seedning == "X" ? "" : deltager.Seedning;
						d.PB = deltager.Pr == "X" ? "" : deltager.Pr;
						d.Comment = deltager.Kommentar;
						d.Qualified = deltager.QualifiedText;

						if (deltager.ResKodeText == "")
						{
							d.Position = deltager.ResStatus.IsNull() ? deltager.Plac.ToString() : "";
							d.Result = deltager.Resultat;
						}
						else
						{
							d.Position = "";
							d.Result = deltager.ResKodeText;
						}

						doBuildSerie(ov, deltager, d);

						h.Participants.Add(d);
					}
				}

				ResultList.Heats.Add(h);
			}

			if (ov.ResultatSort == "Resultat" && ov.AntalHeat > 1)
			{
				List<ResDeltager> delt = new List<ResDeltager>();
				foreach (Heat heat in ResultList.Ov.Heats)
				{
					if (heat.Nr != 0)
					{
						if (heat.Deltagere != null)
						{
							foreach (ResDeltager deltager in heat.Deltagere)
							{
								if (deltager.ResKodeText == "" && deltager.ResStatus.IsNull())
									delt.Add(deltager);
							}
						}
					}
				}

				if (delt.Count > 0)
				{
					ResultList.Summary = new List<DispParticipant>();
					ResultList.SummaryHeader = "SUMMARY";

					delt.Sort((k1, k2) => k1.CompareResultListPosition(k2));

					foreach (ResDeltager deltager in delt)
					{
						DispParticipant d = new DispParticipant();

						mars_MTeams key = teamList.FirstOrDefault(t => deltager.Klub == t.ShortName);
						d.TeamId = key != null ? key.TeamId : Guid.Empty;

						d.OrderNo = deltager.BaneNr.ToString();
						d.BibNo = StartList.Ov.StafetOvelse ? "" : deltager.StartNr.ToString();
						d.Name = StartList.Ov.StafetOvelse ? deltager.Navn : deltager.EfternavnFornavn;
						d.YOB = StartList.Ov.StafetOvelse ? "" : deltager.Aargang;
						d.Team = deltager.Klub;
						d.SB = deltager.Seedning == "X" ? "" : deltager.Seedning;
						d.PB = deltager.Pr == "X" ? "" : deltager.Pr;
						d.Comment = deltager.Kommentar;

						if (deltager.ResKodeText == "")
						{
							d.Position = deltager.ResStatus.IsNull() ? deltager.Plac.ToString() : "";
							d.Result = deltager.Resultat;
						}
						else
						{
							d.Position = "";
							d.Result = deltager.ResKodeText;
						}

						ResultList.Summary.Add(d);
					}
				}
			}
		}

		public void BuildResultList(List<mars_MTeams> teamList)
		{
			if (Staevne == null || Staevne.Ovelser == null || Staevne.Ovelser.Length != 1)
				return;

			Ovelse ov = Staevne.Ovelser[0];
			if (ov.Heats == null)
				return;

			if (StateText.StartsWith("N"))
				return;

			if (StateText.StartsWith("O") && !ov.LobeOvelse) doBuildOngoingResultList(ov, teamList);
			else if (ov.FindesResultat) doBuildResultList(ov, teamList);
		}
	}

	public class RecordList
	{
		public Ovelse Ov { get; set; }
		public List<DispRecord> Lines { get; set; }
	}

	public class ResultList
	{
		public Ovelse Ov { get; set; }

		public List<DispParticipant> Summary { get; set; }

		public List<DispHeat> Heats { get; set; }
		public string SummaryHeader { get; set; }

		public int NoInSerie { get; set; }
		public List<string> JumpSchema { get; set; }
	}

	public class StartList
	{
		public Ovelse Ov { get; set; }
		public List<DispHeat> Heats { get; set; }
	}

	public class DispHeat
	{
		public string Header { get; set; }
		public List<DispParticipant> Participants { get; set; }
	}

	public class DispParticipant
	{
		public Guid ParticipantId { get; set; }

		public string OrderNo { get; set; }
		public string Lane { get; set; }
		public string BibNo { get; set; }
		public string SB { get; set; }
		public string PB { get; set; }
		public string Name { get; set; }
		public string YOB { get; set; }
		public string Team { get; set; }

		public string Position { get; set; }
		public string Qualified { get; set; }

		public string Result { get; set; }       // MarkText
		public string ResultCode { get; set; }   // MarkCode
		public string Comment { get; set; }      // MarkRemark

		public string ResultCodeText             // MarkCodeText
		{
			get
			{
				return ResultCode.IsNull() ? "" : MeetModel.AllowedXMarks[ResultCode];
			}
		}
		public string ResultOutput
		{
			get
			{
				return ResultCode.IsNull() ? Result : ResultCodeText;
			}
		}

		public OneEvent OneEvent { get; set; }
		public Guid TeamId { get; set; }

		public List<string> Serie1 { get; set; }
	}

	public class DispRecord
	{
		public string ID { get; set; }
		public string Result { get; set; }
		public string Name { get; set; }
	}

	public class MedalCompetition
	{
		public List<CompetitionTeam> Teams { get; set; }
	}

	public class PointsCompetition
	{
		public List<CompetitionTeam> Teams { get; set; }
	}

	public class CompetitionTeam
	{
		public string Name { get; set; }
		public int[] Count { get; set; }
	}
}
