function SetHeight()
{
	if (document.documentElement.clientHeight > 199)
	{
		$get("left").style.height = document.documentElement.clientHeight - 152 + "px";
		$get("right").style.height = document.documentElement.clientHeight - 143 + "px";       	
		$get("usersright").style.height = document.documentElement.clientHeight - 199 + "px";
		$get("messages").style.height = document.documentElement.clientHeight - 194 + "px";
		$get("messages").scrollTop = $get("messages").scrollHeight;
	}
	if (document.documentElement.clientWidth > 200)
	{
		$get("left").style.width = document.documentElement.clientWidth - 200 + "px";
	    $get("txtSendMessage").style.width = document.documentElement.clientWidth - 100 + "px";
	}
	
    var loginBox = $get("loginBox");
    loginBox.style.left = (loginBox.offsetParent.clientWidth/2)-(loginBox.clientWidth/2)+loginBox.offsetParent.scrollLeft + "px";
    loginBox.style.top = (loginBox.offsetParent.clientHeight/2)-(loginBox.clientHeight/2)+loginBox.offsetParent.scrollTop + "px";
}