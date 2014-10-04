@echo off

set mars_database=mars_net_dk_db.dbo.mars_
set options=-S(localdb)\V11.0 -T -n -q

bcp %mars_database%CRecordMarks in CRecordMarks.bcp %options%
bcp %mars_database%CRecords in CRecords.bcp %options%
bcp %mars_database%CCombinedEvents in CCombinedEvents.bcp %options%
bcp %mars_database%CSubEvents in CSubEvents.bcp %options%
bcp %mars_database%CEvents in CEvents.bcp %options%
bcp %mars_database%CGroupEvents in CGroupEvents.bcp %options%
bcp %mars_database%CGroups in CGroups.bcp %options%

bcp %mars_database%MMarks in MMarks.bcp %options%
bcp %mars_database%MEventEntries in MEventEntries.bcp %options%
bcp %mars_database%MGroups in MGroups.bcp %options%
bcp %mars_database%MParticipants in MParticipants.bcp %options%
bcp %mars_database%MTeams in MTeams.bcp %options%
bcp %mars_database%MMeet in MMeet.bcp %options%

pause
