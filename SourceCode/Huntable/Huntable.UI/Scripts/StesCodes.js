/*
StesCodes Contact Grabber Signature Creator
Copyright (c) 2012  www.stescodes.com
You can use the script on you website for free but this comment part should not be removed.
version 5.0 (Mar 3,2012)
Authors: StesCodes(Navin leon)
*/
function stes(){var d="",f=function(c,a){var b=new Date;b.setTime(b.getTime()+864E5);b="; expires="+b.toGMTString();document.cookie=c+"="+a+b+"; path=/"},g=function(c){var a,b,d,e=document.cookie.split(";");for(a=0;a<e.length;a++)if(b=e[a].substr(0,e[a].indexOf("=")),d=e[a].substr(e[a].indexOf("=")+1),b=b.replace(/^\s+|\s+$/g,""),b==c)return unescape(d)};return{getSignatureKey:function(){return d},init:function(c){var a=g("sigKey"),b=g("lic");if(null==b||""==b||void 0==b||b!=c)f("lic",c),a="";null==a || "" == a || void 0 == a ? (c = "http://stescodes.com/checkupdate.aspx?formaturl=" + window.location + "&signature=" + c + "&callback=StesCodes.load",
a=document.createElement("script"),a.setAttribute("src",c),document.getElementsByTagName("head")[0].appendChild(a)):d=a},load:function(c){null==c.error&&(d=c.signature,f("sigKey",d))}}}var StesCodes=new stes;