/*提示JavaScript 返回顶部插件 by Denglh 2010-10-27*/
jQuery.fn.scrollTo = function(speed) {
	var $Offset = $(this).offset().top;
	$('html,body').stop().animate({scrollTop: $Offset}, speed);
	return this;
}; 
/*Tab切换 by Denglh 2010-11-06
------------------------------------------------------------------
参数：| control:控制节点；
	 | content:容器ID；
	 | mode:可选事件["click","mouseover"]
	 | showall:是否在点击第一个时展示所有容器子元素，值为true|false;
	 | current:当前状态样式名称
	 -------------------------------------------------------------
示例：|tabshow("#tab>a","#tabcontent","click",true,"active")
	 |tabshow("#tab>li","#tabcontent","mouseover",false,"current")
------------------------------------------------------------------*/
function tabshow (control,content,mode,showall,current) {
	$(control).live(mode, function(){
		$(this).blur().addClass(current).siblings().removeClass(current);
		var $index = $(control).index(this);
		//alert($index);
		if(showall==true){
			if($index == 0) {
				$(content).children().show();
				}else{
				$(content).children().eq($index-1).show().siblings().hide();
			}
		}else{
			$(content).children().eq($index).show().siblings().hide();
		}
		if(mode=="mouseover" && $(this).attr("href")=="#"){
			return false;
		}else if(mode=="click" && $(this).attr("href")=="#"){
			return false;
		}else{
			return;
		}
	}).hover(
		function(){
			if($(this).hasClass(current)){
				$(this).addClass("");
			}else{
				$(this).addClass("hover");
			}
	},function(){
		$(this).removeClass("hover");
	});

 }

function jQFocus(obj,time,speed,auto) {
	var t = n =0,li=$(obj+" a"),count = $(obj+" a").size();
		$(".ifocus_title").html($(".ifocus_list a:first-child").find("img").attr('alt'));
		$(obj+" .ifocus_control a:first-child").addClass("flashpic_numon");
		$(".ifocus_title").attr("href",$(".ifocus_list a:first-child").attr('href'));
	$(obj+" .ifocus_control span").click(function() {
			var i = $(this).text() - 1;
				n = i;
				if (i >= count) return;
				$(".ifocus_list a").filter(":visible").fadeOut(speed).parent().children().eq(i).fadeIn(speed);
				$(".ifocus_title").html($(".ifocus_list a").eq(i).find("img").attr('alt'));
				$(".ifocus_title").attr("href",$(".ifocus_list a").eq(i).attr('href'));
				$(this).addClass("flashpic_numon").siblings().removeClass("flashpic_numon");
		});
		if(auto==true ) {
			showAuto = function (){
				n = n >= (count - 1) ? 0 : ++n;
				$(obj+" .ifocus_control span").eq(n).trigger('click');
			}
			t = setInterval("showAuto()", time);
			$(obj).hover(function(){clearInterval(t)}, function(){t = setInterval("showAuto()", time);});	
		}
}

//弹出提示
var showinfo = function(str,obj){
	if ($("#showinfo").length<=0){
		$("body").append("<div class='showinfo' id='showinfo'></div>");
	}
	var objDiv = $("#showinfo");
	if(!objDiv.is(":animated")){ 
		var time = showinfo.arguments[2];
		var left = showinfo.arguments[3];
		
		var top = showinfo.arguments[4];
		
		if(cint(time)==0) time=1000;
		if(cint(left)==0) left=20;
		if(cint(top)==0) top=8;
		var tip_x = obj.offset().left+left;
		var tip_y = obj.offset().top+top+"px";
		
//		var tip_x = obj.offset().left-76;
//	    var tip_y = obj.offset().top+23+"px";
		var tip_w = obj.width();
		objDiv.text(str)
		.css({"top":tip_y,"left":tip_x+tip_w+"px"})
		.fadeIn(50).fadeOut(time);	
	}
}
var cint=function(intstr){
	var intstr_ = parseInt(intstr);
	if(isNaN(intstr_)) intstr_ = 0;
	return intstr_;
}
 
 /*---------------------------------------------------------------------
//jQuery焦点图切换 By Denglh [2010-11-08]
//----------------------------------------------------------------------
	  obj:	要切换的对像[ID,Class]
	 time:	设置每次动画执行间隔时间 [单位毫秒)]
	speed:	设置每次动画执行时间 [单位毫秒]
	 auto:	设置是否显自动切换 [可选值为:true,false]
------------------------------------------------------------------------*/
function jQFocus(obj,time,speed,auto) {
	var t = n =0,li=$(obj+" a"),count = $(obj+" a").size();
		$(".ifocus_title").html($(".ifocus_list a:first-child").find("img").attr('alt'));
		$(obj+" .ifocus_control a:first-child").addClass("flashpic_numon");
		$(".ifocus_title").attr("href",$(".ifocus_list a:first-child").attr('href'));
	$(obj+" .ifocus_control span").click(function() {
			var i = $(this).text() - 1;
				n = i;
				if (i >= count) return;
				$(".ifocus_list a").filter(":visible").fadeOut(speed).parent().children().eq(i).fadeIn(speed);
				$(".ifocus_title").html($(".ifocus_list a").eq(i).find("img").attr('alt'));
				$(".ifocus_title").attr("href",$(".ifocus_list a").eq(i).attr('href'));
				$(this).addClass("flashpic_numon").siblings().removeClass("flashpic_numon");
		});
		if(auto==true ) {
			showAuto = function (){
				n = n >= (count - 1) ? 0 : ++n;
				$(obj+" .ifocus_control span").eq(n).trigger('click');
			}
			t = setInterval("showAuto()", time);
			$(obj).hover(function(){clearInterval(t)}, function(){t = setInterval("showAuto()", time);});	
		}
}
 
 //email,mobile,tel,idcard,num,pwd,empty
;(function(){
	$.L_validator=function(ele_form,options){
		if(ele_form[0].nodeName.toLowerCase()=="form"){
			$('[ctype]').each(function(){
				$(this).blur(function(){
					$.L_validator.classRules(this);
					if(!$.L_validator.vali_result){return false};
				});
			});
			ele_form.bind("falseSubmit",function(){
				$('[ctype]').each(function(){
					$(this).blur();
					if(!$.L_validator.vali_result) return false;
				});
			});
			ele_form.submit(function(){
				ele_form.trigger("falseSubmit");
				if(!$.L_validator.vali_result)return false;
				else{return true;}
			});
		}
	};
	$.extend($.L_validator,{
		methods:{//验证方法
			must:function(ele){return $.trim($(ele).val()).length>0;},
			username:function(ele){return /^[A-Za-z]\w{2,14}$/.test($(ele).val())},
			email:function(ele){return  /^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$/.test($(ele).val())},
			int:function(ele){return /^[0-9]*[1-9]*[0-9]*$/.test($(ele).val())},
			money:function(ele){return /^(0|[1-9][0-9]*)(.[0-9]{1,2})?$/.test($(ele).val())},
			_money:function(ele){return /^(\-{0,1})(0|[1-9][0-9]*)(.[0-9]{1,2})?$/.test($(ele).val())},
			mobile:function(ele){return /^((\(\d{2,3}\))|(\d{3}\-))?1[3,5,8]\d{9}$/.test($(ele).val())},
			tel:function(ele){return /^((\(\d{2,3}\))|(\d{3}\-))?(\(0\d{2,3}\)|0\d{2,3}-)?[1-9] \d{6,7}(\-\d{1,4})?$/.test($(ele).val())},
			idcard:function(ele){return chkIdCard($(ele).val())==0;},
			pwd:function(ele){return /^\w{6,20}$/.test($(ele).val())},
			repeat:function(ele){return	$('input[id='+$(ele).attr("pwd_target")+']').val()==$(ele).val()},
			maxlen:function(ele){return	parseInt($(ele).val().length)<=parseInt($(ele).attr("maxlength"));},
			minnum:function(ele){return	parseInt($(ele).val())>=parseInt($(ele).attr("minnum"));},
			maxnum:function(ele){return	parseInt($(ele).val())<=parseInt($(ele).attr("maxnum"));}
		},
		classRules:function(element){//验证方法_结果表现
			if(!$(element).attr('ctype'))return false;
			var ctypes=$(element).attr('ctype').split(' ');
			if($(element).attr('message'))var messages=$(element).attr('message').split(' ');
			else var messages=[];
			$.L_validator.ele=element;
			$.each(ctypes,function(ind){
				if (this in $.L_validator.methods){
					if (!new Function("return $.L_validator.methods."+this+"($.L_validator.ele)")()){
						$.L_validator.vali_result = false;
						$.L_showinfo(element,{message:messages[ind],time:2000});
						$(element).addClass("ipterr");
						return false;
					}else{
						$.L_validator.vali_result = true;
						$(element).removeClass("ipterr");
					}
				}
			});
		},
		vali_result:true//验证结果,通过此变量判断表单提交与否
	});
	$.extend({
		L_showinfo:function(element,options){//错误信息提示
			if(!element)return;
			options=$.extend({
				left:5,
				top:0,
				time:1000
			},options);
			options.message=options.message||"请输入正确的内容";
			//重新处理提示方式
			var $showinfo=$('#showinfo');
			if($showinfo.length==0){
				$showinfo=$('<span id="showInfo" style="background:#C00; color:#fff; height:14px; line-height:14px; padding:5px 15px 5px 5px; position:absolute; display:none;"></span>');
				$('body').append($showinfo);
			}
			if(!$showinfo.is(":animated")){
				var x = $(element).offset().left+options.left;
				var y = $(element).offset().top+options.top;
				var h = $(element).outerHeight();
				$showinfo.text(options.message)
				.css({"top":y+h,"left":x})
				.fadeIn(50).fadeOut(options.time);
			}
			///////////////////////////////////////////////////
		}
	});
	$.fn.extend({
		L_validate:function(options){//验证方法_事件处理
			$.L_validator(this,options);
		}
	});
	$.fn.extend({
		L_showInfo:function(options){//验证方法_事件处理
			$.L_showinfo(this,options);
		}
	});
})(jQuery);


  var info = function(str,obj){
  
		
	if ($("#info").length<=0){
		$("body").append("<div class='showInfo'  id='info'></div>");
		$("#info").css("background","#03b1fa");
		$("#info").css("color","#fff");
		$("#info").css("line-height","14px");
		$("#info").css("height","14px");
		$("#info").css("padding","5px 15px 5px 5px");
		$("#info").css("position","absolute");
		$("#info").css("display","none");
		$("#info").css("z-index","9999");
	} 
	var objDiv = $("#info");
	if(!objDiv.is(":animated")){ 
		var bg=info.arguments[2];                 //设置背景颜色
		var time = info.arguments[3];             //设置停留时间
		var left = info.arguments[4];			     //设置左停靠距离
		var top = info.arguments[5];              //设置上停靠距离
		var id=info.arguments[6];                 //设置控件的边框颜色
//      $(id).css({"border-color":"red","border-style":"solid","border-width":"1px"}).fadeIn(100).fadeOut(2000);
		$(id).css({"border-color":"red","border-style":"solid","border-width":"1px"}).fadeTo(2000,1,function(){ $(id).css({"border-color":"Gray","border-style":"solid","border-width":"1px"}); });
		if(cint(time)==0) time=2000;
		if(cint(left)==0) left=0;//20;
		if(cint(top)==0) top=0;//8;
		var tip_x = obj.offset().left+left-obj.width();
		var tip_y = obj.offset().top+top+obj.height()+5+"px";
		var tip_w = obj.width();
	   
		if(bg!="")
			objDiv.css("background",bg);
		objDiv.text(str)
		.css({"top":tip_y,"left":tip_x+tip_w+"px"})
		.fadeIn(100).fadeOut(time);

	  }
	}
	
/*
页面插入FLASH by Huang Jincheng 2010-10-29
参数：ur| FLASH的地址
	  w| FLASH的宽度
	  h| FLASH的高度
example:| <script language="javascript" type="text/javascript">flash('plane.swf','800','600');</script>
*/
function flash(ur,w,h){ 
document.write('<object classid="clsid:D27CDB6E-AE6D-11CF-96B8-444553540000" id="obj1" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,40,0" border="0" width="'+w+'" height="'+h+'">'); 
document.write('<param name="movie" value="'+ur+'">'); 
document.write('<param name="quality" value="high"> '); 
document.write('<param name="wmode" value="transparent"> '); 
document.write('<param name="menu" value="false"> '); 
document.write('<embed src="'+ur+'" pluginspage="http://www.macromedia.com/go/getflashplayer" type="application/x-shockwave-flash" name="obj1" width="'+w+'" height="'+h+'" quality="High" wmode="transparent"/>');  
document.write('</object>'); 
} 


 