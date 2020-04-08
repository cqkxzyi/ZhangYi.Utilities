
//是否为PC端口
function IsPC() {
	var userAgentInfo = navigator.userAgent;
	var Agents = ["Android", "iPhone",
		"SymbianOS", "Windows Phone",
		"iPad", "iPod"];
	var flag = true;
	for (var v = 0; v < Agents.length; v++) {
		if (userAgentInfo.indexOf(Agents[v]) > 0) {
			flag = false;
			break;
		}
	}
	return flag;
}

//是否IOS端
function IsIOS(){
	 if (navigator.userAgent.match(/(iPhone|iPod|iPad);?/i)){
		 
		 return true;
	 }
	return false;
}
//是否Android端
function IsAndrid(){
	 if (navigator.userAgent.match(/(Android);?/i)){
		 return true;
	 }
	return false;
}

//是否微信浏览器
function IsAndrid(){
	 var ua = navigator.userAgent.toLowerCase();
	 if (ua.match(/MicroMessenger/i) == "micromessenger") {
		 return true;
	 }
	 return false;
}

 







//复制
function copy(content) {
	if (navigator.userAgent.match(/(iPhone|iPod|iPad);?/i)) {//区分iPhone设备
		var copyDOM = document.getElementById("copy-pp");  //要复制文字的节点
		selectText(copyDOM, 0, 2);
		document.execCommand('copy');
	}
	else {
		androidCopy(content);
	}
}

//android复制
function androidCopy(content) {
	//使用textarea支持换行，使用input不支持换行
	var textarea = document.createElement('textarea');
	textarea.value = content;
	document.body.appendChild(textarea);
	textarea.select();
	if (document.execCommand('copy')) {
		document.execCommand('copy');
		alert("复制成功，打开微信选择好友，粘贴即可。");
	}
	document.body.removeChild(textarea);
}

function selectText(textbox, startIndex, stopIndex) {
	if (textbox.createTextRange) {//ie
		const range = textbox.createTextRange();
		range.collapse(true);
		range.moveStart('character', startIndex);//起始光标
		range.moveEnd('character', stopIndex - startIndex);//结束光标
		range.select();//不兼容苹果
	} else {//firefox/chrome
		textbox.setSelectionRange(startIndex, stopIndex);
		textbox.focus();
	}
}

