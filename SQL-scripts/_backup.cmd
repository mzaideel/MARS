@echo off

set mars_database=mars_net_dk_db.dbo.mars_
set options=-S(localdb)\V11.0 -T -n

bcp %mars_database%CCombinedEvents out CCombinedEvents.bcp %options%
bcp %mars_database%CEvents out CEvents.bcp %options%
bcp %mars_database%CGroupEvents out CGroupEvents.bcp %options%
bcp %mars_database%CGroups out CGroups.bcp %options%
bcp %mars_database%CRecordMarks out CRecordMarks.bcp %options%
bcp %mars_database%CRecords out CRecords.bcp %options%
bcp %mars_database%CSubEvents out CSubEvents.bcp %options%
bcp %mars_database%CRecords out CRecords.bcp %options%
bcp %mars_database%CRecordMarks out CRecordMarks.bcp %options%

bcp %mars_database%MEventEntries out MEventEntries.bcp %options%
bcp %mars_database%MGroups out MGroups.bcp %options%
bcp %mars_database%MMarks out MMarks.bcp %options%
bcp %mars_database%MMeet out MMeet.bcp %options%
bcp %mars_database%MParticipants out MParticipants.bcp %options%
bcp %mars_database%MTeams out MTeams.bcp %options%

pause
