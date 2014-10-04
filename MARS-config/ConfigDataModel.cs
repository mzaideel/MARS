using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace MARS.Config
{
	//[DataObject]
	public partial class MARS_Config_dbEntities : DbContext
	{
		//	#region Constructors
		//	private const string name = "MARS_Config_dbEntities";

		//	public ConfigDataModel() :
		//		this(String.Format("name={0}", name))
		//	{
		//	}

		//	public ConfigDataModel(bool doLogging) :
		//		this(String.Format("name={0}", name), doLogging)
		//	{
		//	}

		//	public ConfigDataModel(string connectionString)
		//		: this(connectionString, true)
		//	{
		//	}

		//	public ConfigDataModel(string connectionString, bool doLogging)
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
		//		LogManager.Save("<<<<<< ConfigDataModel_SavingChanges >>>>>>");
		//	}
		//	#endregion

		//	#region Get
		//public List<object> GetAllEvents()
		//{
		//	return (from e in mars_CEvents
		//			join s in mars_CSubEvents on e.EventId equals s.EventId
		//			orderby s.SortKey
		//			select new
		//			{
		//				Event = e,
		//				SubEvent = s
		//			}).ToList<object>();
		//}

		//	public List<object> GetAllEvents(string groupId)
		//	{
		//		if (groupId == null)
		//			return GetAllEvents();

		//		return (from g in mars_CGroups
		//					 join ge in mars_CGroupEvents on g.GroupId equals ge.GroupId
		//					 join s in mars_CSubEvents on new { ge.EventId, ge.SubEventId } equals new { s.EventId, s.SubEventId }
		//					 join e in mars_CEvents on s.EventId equals e.EventId
		//					 where g.GroupId == groupId
		//					 orderby s.SortKey
		//					 select new
		//					 {
		//						 Group = g,
		//						 GroupEvent = ge,
		//						 Event = e,
		//						 SubEvent = s
		//					 }).ToList<object>();
		//	}
		//	#endregion
	}

	//public partial class mars_CEvents : EntityObject, IEntityProcessSaving
	//{
	//	#region IEntityProcessSaving implementation
	//	public void Delete() { }
	//	public void AddStepOne() { }
	//	public void AddStepTwo() { }
	//	public void Validate(EntityState state) { }

	//	public void FieldChanged(string name, object orgValue, object currentValue) { }
	//	public void FieldAdded(string name, object currentValue) { }
	//	public void FieldDeleted(string name, object orgValue) { }
	//	#endregion
	//}

	//public partial class mars_CSubEvents : EntityObject, IEntityProcessSaving
	//{
	//	#region IEntityProcessSaving implementation
	//	public void Delete() { }
	//	public void AddStepOne() { }
	//	public void AddStepTwo()
	//	{
	//		if (Event != null)
	//			EventId = Event.EventId;
	//	}
	//	public void Validate(EntityState state) { }

	//	public void FieldChanged(string name, object orgValue, object currentValue) { }
	//	public void FieldAdded(string name, object currentValue) { }
	//	public void FieldDeleted(string name, object orgValue) { }
	//	#endregion
	//}

	//public partial class mars_CCombinedEvents : EntityObject, IEntityProcessSaving
	//{
	//	#region IEntityProcessSaving implementation
	//	public void Delete() { }
	//	public void AddStepOne() { }
	//	public void AddStepTwo() { }
	//	public void Validate(EntityState state) { }

	//	public void FieldChanged(string name, object orgValue, object currentValue) { }
	//	public void FieldAdded(string name, object currentValue) { }
	//	public void FieldDeleted(string name, object orgValue) { }
	//	#endregion
	//}

	//public partial class mars_CGroups : EntityObject, IEntityProcessSaving
	//{
	//	#region IEntityProcessSaving implementation
	//	public void Delete() { }
	//	public void AddStepOne() { }
	//	public void AddStepTwo() { }
	//	public void Validate(EntityState state) { }

	//	public void FieldChanged(string name, object orgValue, object currentValue) { }
	//	public void FieldAdded(string name, object currentValue) { }
	//	public void FieldDeleted(string name, object orgValue) { }
	//	#endregion
	//}

	//public partial class mars_CGroupEvents : EntityObject, IEntityProcessSaving
	//{
	//	#region IEntityProcessSaving implementation
	//	public void Delete() { }

	//	public void AddStepOne() { }

	//	public void AddStepTwo() 
	//	{
	//		if (Group != null)
	//			GroupId = Group.GroupId;

	//		if (SubEvent != null)
	//		{
	//			EventId = SubEvent.EventId;
	//			SubEventId = SubEvent.SubEventId;
	//		}
	//	}

	//	public void Validate(EntityState state) { }

	//	public void FieldChanged(string name, object orgValue, object currentValue) { }
	//	public void FieldAdded(string name, object currentValue) { }
	//	public void FieldDeleted(string name, object orgValue) { }
	//	#endregion
	//}

	//public partial class mars_CRecords : EntityObject, IEntityProcessSaving
	//{
	//	#region IEntityProcessSaving implementation
	//	public void Delete() { }
	//	public void AddStepOne() { }
	//	public void AddStepTwo() { }
	//	public void Validate(EntityState state) { }

	//	public void FieldChanged(string name, object orgValue, object currentValue) { }
	//	public void FieldAdded(string name, object currentValue) { }
	//	public void FieldDeleted(string name, object orgValue) { }
	//	#endregion
	//}

	//public partial class mars_CRecordMarks : EntityObject, IEntityProcessSaving
	//{
	//	#region IEntityProcessSaving implementation
	//	public void Delete() { }

	//	public void AddStepOne()
	//	{
	//		if (RecordMarkId == null || RecordMarkId == Guid.Empty)
	//			RecordMarkId = Guid.NewGuid();
	//	}

	//	public void AddStepTwo()
	//	{
	//		if (RecordId != null)
	//			RecordId = Record.RecordId;

	//		if (SubEvent != null)
	//		{
	//			EventId = SubEvent.EventId;
	//			SubEventId = SubEvent.SubEventId;
	//		}
	//	}

	//	public void Validate(EntityState state) { }

	//	public void FieldChanged(string name, object orgValue, object currentValue) { }
	//	public void FieldAdded(string name, object currentValue) { }
	//	public void FieldDeleted(string name, object orgValue) { }
	//	#endregion
	//}

	//public enum CResultType
	//{
	//	Centisecond = 1,
	//	Second = 2,
	//	Minute = 3,
	//	Hour = 4,
	//	Centimetre = 5,
	//	Metre = 6,
	//	Kilometre = 7,
	//	Points = 8,
	//}

	//public enum CEventType
	//{
	//	Track = 1,
	//	FieldJumpVertical = 2,
	//	FieldJumpHorizontal = 3,
	//	FieldThrow = 4,
	//	TrackRelay = 5,
	//	TrackCombined = 6,
	//	Road = 7,
	//}

	//public class CEventHelper
	//{ 
	//	public static bool IsRunningEvent(CEventType eventType)
	//	{
	//		return eventType.In(CEventType.Track, CEventType.TrackRelay, CEventType.Road);
	//	}

	//	public static bool IsRunningEvent(byte eventType)
	//	{
	//		return IsRunningEvent((CEventType)eventType);
	//	}
	//}
}
