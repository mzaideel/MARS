$(function(){
	
	var note = $('#note'),
		ts = (new Date()).getTime() + 60*1000;
		
	$('#countdown').countdown({
		timestamp	: ts,
		callback	: function(days, hours, minutes, seconds){
			
			var message = seconds;
			note.html(message);

			if (seconds == 0)
			{
				location.reload();
			}
		}
	});
	
});
