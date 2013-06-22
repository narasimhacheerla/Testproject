<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Terms.aspx.cs" Inherits="Huntable.UI.Terms" %>

<%@ Register Src="~/UserControls/topNews.ascx" TagPrefix="uc1" TagName="news" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Huntable - The Professional Network</title>
    <meta name="description" content="Huntable - The Professional Network" />
    <meta name="keywords" content="The Professional Network" />
    <link href="css/style-innerpage.css" type="text/css" rel="stylesheet" />
    
<style type="text/css">

#slidebox{position:relative;  }
#slidebox, #slidebox .content{width:650px; margin-bottom:20px;}
#slidebox, #slidebox .container, #slidebox .content{height:230px;}
#slidebox{overflow:hidden;}
#slidebox .container{position:relative; left:0;}
#slidebox .content{background:#eee; float:left;}
#slidebox .content div{ height:100%; font-family:Verdana, Geneva, sans-serif; font-size:13px;}
#slidebox .next, #slidebox .previous{position:absolute; z-index:2; display:block; width:21px; height:21px;}
#slidebox .next{right:0; margin-right:10px; background:url(slidebox_next.png) no-repeat left top;}
#slidebox .next:hover{background:url(slidebox_next_hover.png) no-repeat left top;}
#slidebox .previous{margin-left:10px; background:url(slidebox_previous.png) no-repeat left top;}
#slidebox .previous:hover{background:url(slidebox_previous_hover.png) no-repeat left top;}
#slidebox .thumbs{position:absolute; z-index:2; bottom:10px; right:10px;}
#slidebox .thumbs .thumb{display:block; margin-left:5px; float:left; font-family:Verdana, Geneva, sans-serif; font-size:9px; text-decoration:none; padding:2px 4px; background:url(slidebox_thumb.png); color:#fff;}
#slidebox .thumbs .thumb:hover{background:#fff; color:#000;}
#slidebox .selected_thumb{background:#fff; color:#000; display:block; margin-left:5px; float:left; font-family:Verdana, Geneva, sans-serif; font-size:9px; text-decoration:none; padding:2px 4px;}

.termsitalic{
	text-align:center;
	font-weight:bold;
	font-style:italic;
	font-size:18px;
}


</style>
  
    
    </head>



 

<!-- this script used for both clickable slide and tab slide function -->




<div id="content-section" style="background-color:White;margin-top: 80px;">
      <div id="content-inner">
      
      <div class="accounts-profile2 accounts-profile3">
      <p class="terms-heading">HUNTABLE LIMITED<br />
TERMS AND CONDITIONS OF USE OF OUR WEBSITE<br /><br />

THESE TERMS AND CONDITIONS ARE INTENDED TO FORM A BINDING CONTRACT BETWEEN YOU (OUR VISITOR) AND US (THE SITE AND THE OWNERS OF THE SITE). YOU ARE ADVISED TO READ THEM THROUGH THOROUGHLY BEFORE VISITNG THE REMAINDER OF THE SITE AS THEY CONTAIN <br />
CONDITIONS WHICH BIND YOU.<br /><br />
IF ANY OF THESE TERMS IS NOT ACCEPTABLE TO YOU - YOU MUST LEAVE THE SITE IMMEDIATELY AS YOUR AUTHORITY TO ACCESS IT IS DENIED.<br /><br /></p>


<div class="experience">
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td colspan="2" align="left" valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td align="left" valign="top">&ldquo;Huntable&rdquo; </td>
        <td align="left" valign="top">are  Huntable.co.uk this site and any sub-site to which it allows access and which  is owned by the Owner and which does not have its own terms and conditions</td>
      </tr>
      
       <tr>
    <td align="left" valign="top">&ldquo;Profile&rdquo;</td>
    <td align="left" valign="top">is  the Professional profile produced using this site</td>
  </tr>
  <tr>
    <td align="left" valign="top">&ldquo;The  Owner&rdquo;</td>
    <td align="left" valign="top">Huntable  Limited a company registered in England and Wales under company number 8365108  and whose registered address is 1st Floor 2 Woodberry Grove Finchley  London N12 0DR</td>
  </tr>
  <tr>
    <td align="left" valign="top">&ldquo;Us&rdquo; &ldquo;We&rdquo;       </td>
    <td align="left" valign="top">means either Huntable or the  Owner or both</td>
  </tr>
  <tr>
    <td align="left" valign="top">&ldquo;You&rdquo;  &ldquo;Your&rdquo;</td>
    <td align="left" valign="top"><p>are a visitor to  our website for any purpose whatsoever</p></td>
  </tr>
  <tr>
    <td align="left" valign="top">&ldquo;Job  Credits&rdquo;</td>
    <td align="left" valign="top">means  the right to advertise a specific number of job vacancies on the Site  attributable to the cost and as advertised on the Site from time to time</td>
  </tr>
  <tr>
    <td align="left" valign="top">&ldquo;Payment  1&rdquo;</td>
    <td align="left" valign="top"><p>is a payment  of  US$4.00</p></td>
  </tr>
  <tr>
    <td align="left" valign="top">&ldquo;Payment  2&rdquo;</td>
    <td align="left" valign="top">is  a payment of US$1.00</td>
  </tr>
  <tr>
    <td align="left" valign="top">&ldquo;Payment  3&rdquo;</td>
    <td align="left" valign="top">is  a payment of US$0.50</td>
  </tr>
  <tr>
    <td align="left" valign="top">&ldquo;Premium  Member&rdquo;</td>
    <td align="left" valign="top"><p>will have the  meaning defined in clause 10 of this Agreement.</p></td>
  </tr>
  <tr>
    <td align="left" valign="top"> &ldquo;Registration Fee&rdquo;  </td>
    <td align="left" valign="top"><p>&rdquo;  is the fee We  charge from time to time to register as a Premium Member of the Site</p></td>
  </tr>
  <tr>
    <td align="left" valign="top">&ldquo;Site&rdquo;</td>
    <td align="left" valign="top"><p>is the site you  are now visiting known as Huntable and operated by the Owner</p></td>
  </tr>
  <tr>
    <td align="left" valign="top">&ldquo;Software&rdquo;</td>
    <td align="left" valign="top"><p>means the  computer software We supply to You from time to time</p></td>
  </tr>
  <tr>
    <td align="left" valign="top">&ldquo;Vacancy&rdquo;</td>
    <td align="left" valign="top">means  any vacancy for a position of employment advertised on the Site</td>
  </tr>
      
      
      
      
      
      
      
      
      
    </table></td>
    </tr>
 
 
 
 
 
 
 
 
 
 
 
 
 
  <tr>
    <td width="7%" align="left" valign="top">&nbsp;</td>
    <td width="93%" align="left" valign="top">&nbsp;</td>
  </tr>
  <tr align="left" valign="top">
    <td colspan="2"><p><strong>1. THE CONTRACT</strong></p></td>
    </tr>
  <tr align="left" valign="top">
    <td colspan="2"><p>1. In  consideration of Huntable and the Owner allowing You access to this website You  agree to comply with these Terms and Conditions without any modification.</p></td>
    </tr>
  <tr>
    <td align="left" valign="top">&nbsp;</td>
    <td align="left" valign="top">&nbsp;</td>
  </tr>
  <tr align="left" valign="top">
    <td colspan="2"><p><strong>2. YOUR OBLIGATIONS </strong></p></td>
    </tr>
  <tr align="left" valign="top">
    <td colspan="2"><p>2.1 You warrant  as follows, that:</p></td>
    </tr>
  <tr>
    <td align="left" valign="top">2.1.1</td>
    <td align="left" valign="top">You  have the legal authority to enter into this Contract</td>
  </tr>
  <tr>
    <td align="left" valign="top">2.1.2</td>
    <td align="left" valign="top">You  are 18 years of age or older</td>
  </tr>
  <tr>
    <td align="left" valign="top">2.1.3</td>
    <td align="left" valign="top">You  will read and understand the terms and conditions attached to any site with  which Huntable has links and which you visit by use of such links</td>
  </tr>
  
  
  <tr>
    <td align="left" valign="top">&nbsp;</td>
    <td align="left" valign="top">&nbsp;</td>
  </tr>
  <tr align="left" valign="top">
    <td colspan="2"><p>2.2 You will not </p></td>
    </tr>
  <tr>
    <td align="left" valign="top">2.2.1</td>
    <td align="left" valign="top"><p align="left">Use this Website or its contents  for any commercial purpose</p></td>
  </tr>
  <tr>
    <td align="left" valign="top">2.2.2</td>
    <td align="left" valign="top"><p align="left">access,  monitor or copy any content or information of this Site using any robot,  spider, scraper or other automated means or any manual process for any purpose  without our express written permission</p></td>
  </tr>
  <tr>
    <td align="left" valign="top">2.2.3</td>
    <td align="left" valign="top"><p align="left">violate  the restrictions in any robot exclusion headers on this Website or bypass or  circumvent other measures employed to prevent or limit access to this Website</p></td>
  </tr>
  <tr>
    <td align="left" valign="top">2.2.4</td>
    <td align="left" valign="top">take  any action that imposes, or may impose, in our discretion, an unreasonable or  disproportionately large load on our infrastructure</td>
  </tr>
  <tr>
    <td align="left" valign="top">2.2.5</td>
    <td align="left" valign="top">deep-link  to any portion of this Website for any purpose without our express written  permission</td>
  </tr>
  <tr>
    <td align="left" valign="top">2.2.6</td>
    <td align="left" valign="top">&quot;frame&quot;,  &quot;mirror&quot; or otherwise incorporate any part of this Website into any  other website without our prior written authorisation</td>
  </tr>
  <tr>
    <td align="left" valign="top">2.2.7</td>
    <td align="left" valign="top">attempt  to modify, translate, adapt, edit, decompile, disassemble, or reverse engineer  any software programs used By Huntable in connection with this Site or the  services it offers</td>
  </tr>
  <tr>
    <td align="left" valign="top">&nbsp;</td>
    <td align="left" valign="top">&nbsp;</td>
  </tr>
  <tr align="left" valign="top">
    <td colspan="2"><p><strong>3. INTELLECTUAL PROPERTY</strong></p></td>
    </tr>
  <tr align="left" valign="top">
    <td colspan="2"><p>3.  All information on and all and any Profile  created using this Site belongs to Us </p></td>
    </tr>
  <tr>
    <td align="left" valign="top">3.1 </td>
    <td align="left" valign="top"><p>save where specifically authorised by Us you cannot copy, share,  transmit, reproduce, post or redistribute all or any part of any of the  information contained on this Site without permission in writing from Us and  without subjecting yourself to any additional terms, whether financial or  otherwise, which We in our absolute discretion impose save and except to  provide access to the Profile using the authorisation of this Site</p></td>
  </tr>
  <tr>
    <td align="left" valign="top">3.2</td>
    <td align="left" valign="top">The  content and information on this Site (including, but not limited to, messages,  data, information, text, music, sound, photos, graphics, video, maps, icons,  software, code or other material), as well as the infrastructure used to  provide such content and information, is owned by Us. You agree not to modify,  copy, distribute, transmit, display, perform, reproduce, publish, license,  create derivative works from, transfer, or sell or re-sell any information,  software, products, or services obtained from or through this Site.</td>
  </tr>
  <tr>
    <td align="left" valign="top">&nbsp;</td>
    <td align="left" valign="top">&nbsp;</td>
  </tr>
  <tr>
    <td colspan="2" align="left" valign="top"><p><strong>4. USE OF THIS SITE</strong></p></td>
    </tr>
  <tr>
    <td align="left" valign="top">4.1</td>
    <td align="left" valign="top"> Use of this Site is free to You on condition  that you comply with these Terms and Conditions.</td>
  </tr>
  <tr>
    <td align="left" valign="top">&nbsp;</td>
    <td align="left" valign="top">&nbsp;</td>
  </tr>
  <tr>
    <td colspan="2" align="left" valign="top"><p>4.2 Use of the Profile produced by this  site is subject to the following conditions:</p></td>
    </tr>
  <tr>
    <td align="left" valign="top">4.2.1</td>
    <td align="left" valign="top">You  will not record or attempt to record the Profile nor to store the Profile or  the code with which it has been created nor link or attempt to link the site on  which the Profile is stored with any other site</td>
  </tr>
  <tr>
    <td align="left" valign="top">4.2.2</td>
    <td align="left" valign="top">that  the Profile will be used only for the purpose of:</td>
  </tr>
  <tr>
    <td align="left" valign="top">4.2.2.1</td>
    <td align="left" valign="top"><p>promoting its author in his/her search for employment or</p></td>
  </tr>
  <tr>
    <td align="left" valign="top">4.2.2.2</td>
    <td align="left" valign="top"><p>reviewing the Profile in connection with a possible application for  work</p></td>
  </tr>
  <tr>
    <td align="left" valign="top">4.2.2.3</td>
    <td align="left" valign="top">connecting  with other networking sites and/or other users including corporate users of  this Site – </td>
  </tr>
  <tr>
    <td align="left" valign="top">&nbsp;</td>
    <td align="left" valign="top">&nbsp;</td>
  </tr>
  <tr>
    <td align="left" valign="top">4.3</td>
    <td align="left" valign="top">You  accept that the Profile and the content of the Profile has not been created by,  amended by or in any way received any input from Us and that We are not  responsible for any inaccuracies falsehoods misleading statements or other  erroneous content contained within the Site</td>
  </tr>
  <tr>
    <td align="left" valign="top">&nbsp;</td>
    <td align="left" valign="top">&nbsp;</td>
  </tr>
  <tr>
    <td colspan="2" align="left" valign="top"><p><strong>5. PRIVACY POLICY/COOKIE  POLICY</strong></p></td>
    </tr>
  <tr>
    <td align="left" valign="top">5.</td>
    <td align="left" valign="top"><p> Huntable will  comply with the Privacy and Cookie Policies it publishes from time to time and  You confirm that you have read and agree with both policies (click here to view  our Privacy Policy and Cookie  Policy)</p></td>
  </tr>
  <tr>
    <td align="left" valign="top">&nbsp;</td>
    <td align="left" valign="top">&nbsp;</td>
  </tr>
  <tr>
    <td colspan="2" align="left" valign="top"><p><strong>6. THE PROFILE</strong></p></td>
    </tr>
  <tr>
    <td align="left" valign="top">6.1</td>
    <td align="left" valign="top">&ldquo;Submissions&rdquo;  will mean any content of whatever nature You submit to Huntable</td>
  </tr>
   <tr>
    <td align="left" valign="top">&nbsp;</td>
    <td align="left" valign="top">&nbsp;</td>
  </tr>
  <tr>
    <td align="left" valign="top">6.2</td>
    <td align="left" valign="top">In  respect of all and any Submissions made by You, You grant Us and our affiliates a non-exclusive,  royalty-free, perpetual, transferable, irrevocable and fully assignable right  to </td>
  </tr>
  <tr>
    <td align="left" valign="top">6.2.1</td>
    <td align="left" valign="top">use,  reproduce, modify, adapt, translate, distribute, publish, create derivative  works from and publicly display and perform such Submissions throughout the  world in any media, now known or hereafter devised; and </td>
  </tr>
  <tr>
    <td align="left" valign="top">6.2.2</td>
    <td align="left" valign="top">use  the name that you submit in connection with such Submission. </td>
  </tr>
  <tr>
    <td align="left" valign="top">&nbsp;</td>
    <td align="left" valign="top">&nbsp;</td>
  </tr>
  <tr>
    <td align="left" valign="top">6.3 </td>
    <td align="left" valign="top"><p>You acknowledge that We may choose to provide attribution  of Your comments or reviews at our discretion. </p></td>
  </tr>
  <tr>
    <td align="left" valign="top">&nbsp;</td>
    <td align="left" valign="top">&nbsp;</td>
  </tr>
  <tr>
    <td align="left" valign="top">6.4</td>
    <td align="left" valign="top">You  further grant Us the exclusive right to pursue at law any person or entity that  violates Your or Our rights in the Submissions by a breach of this Contract and  to retain all or any damages costs and other payments granted in such action</td>
  </tr>
  <tr>
    <td align="left" valign="top">&nbsp;</td>
    <td align="left" valign="top">&nbsp;</td>
  </tr>
  <tr>
    <td align="left" valign="top">6.5</td>
    <td align="left" valign="top">You  agree that all Submissions are non-confidential</td>
  </tr>
  <tr>
    <td align="left" valign="top">&nbsp;</td>
    <td align="left" valign="top">&nbsp;</td>
  </tr>
  <tr>
    <td align="left" valign="top">6.6</td>
    <td align="left" valign="top">You  accept that We have an absolute right to remove and/or withdraw from  publication any Submissions which, in its absolute discretion, it regards as  unsuitable.</td>
  </tr>
  <tr>
    <td align="left" valign="top">&nbsp;</td>
    <td align="left" valign="top">&nbsp;</td>
  </tr>
  <tr>
    <td colspan="2" align="left" valign="top"><p><strong>7. INTERACTIVE AREAS</strong></p></td>
    </tr>
  <tr>
    <td align="left" valign="top"><p>7.1</p></td>
    <td align="left" valign="top">&ldquo;Interactive  Area&rdquo; will mean all parts of the Site which allow comment posting or any other  form of interaction between You and the Site.</td>
  </tr>
  <tr>
    <td align="left" valign="top">&nbsp;</td>
    <td align="left" valign="top">&nbsp;</td>
  </tr>
  <tr>
    <td align="left" valign="top">7.2</td>
    <td align="left" valign="top">The  use of the content of this Site is entirely at Your own risk, and We accept no  responsibility for the accuracy or correctness of any of the content on this  Site. When using the Site You expressly agree not to post, upload to, transmit,  distribute, store, create or otherwise publish through the Site any of the  following:</td>
  </tr>
  <tr>
    <td align="left" valign="top">7.2.1</td>
    <td align="left" valign="top">Any  message, data, information, text, music, sound, photos, graphics, code or any  other material (&quot;Content&quot;) that is false, unlawful, misleading,  libellous, defamatory, obscene, pornographic, indecent, lewd, suggestive,  harassing or advocates harassment of another person, threatening, invasive of  priva<a name="_GoBack" id="_GoBack"></a>cy or publicity rights, abusive, inflammatory,  fraudulent or otherwise objectionable</td>
  </tr>
  <tr>
    <td align="left" valign="top">7.2.2</td>
    <td align="left" valign="top">Content  that is patently offensive to the online community, such as content that  promotes racism, bigotry, hatred or physical harm of any kind against any group  or individual</td>
  </tr>
  <tr>
    <td align="left" valign="top">7.2.3</td>
    <td align="left" valign="top">Content that would constitute,  encourage, promote or provide instructions for conduct of an illegal activity,  criminal offence, give rise to civil liability, violate the rights of any party  in any country in the World, or that would otherwise create liability or  violate any local, state, national or international law.</td>
  </tr>
  <tr>
    <td align="left" valign="top">7.2.4</td>
    <td align="left" valign="top">Content  that provides instructional information about illegal activities such as making  or buying illegal weapons, violating someone's privacy, or providing or  creating computer viruses;</td>
  </tr>
  <tr>
    <td align="left" valign="top">7.2.5</td>
    <td align="left" valign="top"><p align="left">Content  that may infringe any patent, trademark, trade secret, copyright or other  intellectual or proprietary right of any party. In particular, content that  promotes an illegal or unauthorised copy of another person's copyrighted work,  such as providing pirated computer programs or links to them, providing  information to circumvent manufacture-installed copy-protect devices, or  providing pirated music or links to pirated music files;</p></td>
  </tr>
  <tr>
    <td align="left" valign="top">7.2.6</td>
    <td align="left" valign="top"><p align="left">Content  that is pornographic, obscene or in any other way (and in Our sole discretion)  objectionable</p></td>
  </tr>
  <tr>
    <td align="left" valign="top">7.2.7</td>
    <td align="left" valign="top">                Content that impersonates any  person or entity or otherwise misrepresents your affiliation with a person or  entity, including Huntable or the Owner;</td>
  </tr>
  <tr>
    <td align="left" valign="top">7.2.8</td>
    <td align="left" valign="top">Unsolicited  promotions, mass mailings or &ldquo;spamming&rdquo;, transmission of &quot;junk mail&quot;,  &quot;chain letters,&quot; political campaigning, advertising, contests,  raffles, or solicitations;</td>
  </tr>
  <tr>
    <td align="left" valign="top">7.2.9</td>
    <td align="left" valign="top">                Content containing commercial  activities and/or sales without our prior written consent such as contests,  sweepstakes, barter, advertising, and pyramid schemes;</td>
  </tr>
  <tr>
    <td align="left" valign="top">7.2.10</td>
    <td align="left" valign="top">Private  information of any third party, including, without limitation, surname (family  name) addresses, phone numbers, email addresses, Social Security numbers and  credit card numbers.</td>
  </tr>
  <tr>
    <td align="left" valign="top">7.2.11</td>
    <td align="left" valign="top">Contains  restricted or password only access pages, or hidden pages or images (those not  linked to or from another accessible page);</td>
  </tr>
  <tr>
    <td align="left" valign="top">7.2.12</td>
    <td align="left" valign="top">Viruses,  corrupted data or other harmful, disruptive or destructive files;</td>
  </tr>
  <tr>
    <td align="left" valign="top">7.2.13</td>
    <td align="left" valign="top">Content  that is unrelated to the topic of the Interactive Area(s) in which such Content  is posted; or</td>
  </tr>
   <tr>
    <td align="left" valign="top">&nbsp;</td>
    <td align="left" valign="top">&nbsp;</td>
  </tr>
  <tr>
    <td align="left" valign="top">7.2.14</td>
    <td align="left" valign="top">Content  or links to content that, in the sole judgment of the Owner,</td>
  </tr>
  <tr>
    <td align="left" valign="top">7.2.14.1</td>
    <td align="left" valign="top">violates  the previous subsections herein,</td>
  </tr>
  <tr>
    <td align="left" valign="top">7.2.14.2</td>
    <td align="left" valign="top">is  objectionable,</td>
  </tr>
  <tr>
    <td align="left" valign="top">7.2.14.3</td>
    <td align="left" valign="top">restricts  or inhibits any other person from using or enjoying the Interactive Areas or  the Site, or </td>
  </tr>
  <tr>
    <td align="left" valign="top">7.2.14.4</td>
    <td align="left" valign="top">may  expose Us or our affiliates or its users to any prosecution harm or liability  of any type.</td>
  </tr>
  <tr>
    <td align="left" valign="top">&nbsp;</td>
    <td align="left" valign="top">&nbsp;</td>
  </tr>
  <tr>
    <td align="left" valign="top">7.3.1  </td>
    <td align="left" valign="top">We  do not assume any responsibility or liability for any Content posted, stored or  uploaded by You or any third party, or for any loss or damage thereto, nor are We  liable for any mistakes, defamation, slander, libel, omissions, falsehoods,  obscenity, pornography or profanity You may encounter. As a provider of  interactive services, We are not liable for any statements, representations or  Content provided by its users in any public forum, personal home page or other  Interactive Area. </td>
  </tr>
  <tr>
    <td align="left" valign="top">7.3.2</td>
    <td align="left" valign="top"><p align="left">Without accepting any obligation  so to do, We reserve the right to remove, screen, alter, edit or monitor any of  the Content posted to or distributed through any Interactive Area which We, in  Our absolute discretion, decides and which is posted or stored on the Site at  any time and for any reason, and You are solely responsible for creating backup  copies of and replacing any Content you post or store on the Site at Your sole  cost and expense.</p></td>
  </tr>
  <tr>
    <td align="left" valign="top">7.3.3</td>
    <td align="left" valign="top">If  it is determined that You retain moral rights (including rights of attribution  or integrity) in the Content, you hereby declare that </td>
  </tr>
  <tr>
    <td align="left" valign="top">7.3.3.1</td>
    <td align="left" valign="top"><p align="left">You do not require that any  personally identifying information be used in connection with the Content, or  any derivative works of or upgrades or updates thereto; </p></td>
  </tr>
  <tr>
    <td align="left" valign="top">7.3.3.2</td>
    <td align="left" valign="top"><p align="left">You have no objection to the  publication, use, modification, deletion and exploitation of the Content by Us  or Our licensees, successors and assigns; </p></td>
  </tr>
  <tr>
    <td align="left" valign="top">7.3.3.3</td>
    <td align="left" valign="top">You  forever waive and agree not to claim or assert any entitlement to any and all  moral rights of an author in any of the Content; and </td>
  </tr>
  <tr>
    <td align="left" valign="top">7.3.3.4</td>
    <td align="left" valign="top">You  forever release Us, and our licensees, successors and assigns, from any claims  that You could otherwise assert against Us by virtue of any such moral rights.</td>
  </tr>
  <tr>
    <td align="left" valign="top">7.4</td>
    <td align="left" valign="top">If  You use the Interactive Areas or any other part of the Site in violation of the  before-mentioned restrictions We may, in Oursole discretion, terminate or  suspend Your right to use the Interactive Areas and/or the Site or any part or  parts of it</td>
  </tr>
  <tr>
    <td align="left" valign="top">7.5</td>
    <td align="left" valign="top">If  required by any proper request of any Court, Law Enforcement or Government body  or to protect Huntable’s systems and other users, Huntable may access and  disclose any information it considers necessary or appropriate, including,  without limitation, user profile information (i.e. name, email address, etc.),  IP address and traffic information, usage history, and posted Content. Huntable’s  right to disclose any such information in these circumstances will take  precedence over any terms of Huntable’s Privacy Policy.</td>
  </tr>
  
  <tr>
    <td align="left" valign="top">&nbsp;</td>
    <td align="left" valign="top">&nbsp;</td>
  </tr>
  <tr>
    <td colspan="2" align="left" valign="top"><p align="left"><strong>8. JOB POSTINGS</strong></p></td>
    </tr>
  <tr>
    <td align="left" valign="top">8.1</td>
    <td align="left" valign="top">You  may post on this Site as many vacancies as the Job Credits You have purchased  permit</td>
  </tr>  
  <tr>
    <td align="left" valign="top">8.2</td>
    <td align="left" valign="top">Each  posting and its related Job Credit will last for 30 days from the date that the  posting is listed on Our Site and will then expire and the listing will be  cancelled</td>
  </tr>
  <tr>
    <td align="left" valign="top">8.3</td>
    <td align="left" valign="top">You  agree that all vacancies You list are for jobs which exist and are vacant at  the time of the listing </td>
  </tr>
  <tr>
    <td align="left" valign="top">8.4</td>
    <td align="left" valign="top">You  have the right to cancel the listing of any Vacancy at any time but in doing so  the Job Credit associated with that Vacancy will expire</td>
  </tr>
  <tr>
    <td align="left" valign="top">8.5</td>
    <td align="left" valign="top">You  may upload to the Site any images or logos associated with any Vacancy within  the reasonable limits We apply from time to time.</td>
  </tr>  
  <tr>
    <td align="left" valign="top">8.6</td>
    <td align="left" valign="top"><p align="left">You  may not list a new Vacancy by way of amending   a Vacancy which You have already posted</p></td>
  </tr>
  <tr>
    <td align="left" valign="top">8.7</td>
    <td align="left" valign="top"><p align="left">You acknowledge that if We  consider that Your listing is in breach of any of these terms and conditions We  may in our absolute discretion either withdraw it without refunding any Job  Credit or You will change the listing to comply with Our reasonable  requirements</p></td>
  </tr>
  <tr>
    <td align="left" valign="top">&nbsp;</td>
    <td align="left" valign="top">&nbsp;</td>
  </tr>
  <tr>
    <td colspan="2" align="left" valign="top"><p align="left"><strong>9. INDEMNITY</strong></p></td>
    </tr>  
  <tr>
    <td colspan="2" align="left" valign="top"><p align="left">You agree to indemnify Us and our  affiliates, officers, directors, employees and agents from and against all loss  or damage, claims, causes of action, demands, recoveries of any kind or nature  including, but not limited to, reasonable legal and accounting fees, brought by  any third party as a result of:</p></td>
    </tr> 
    <tr>
    <td align="left" valign="top">&nbsp;</td>
    <td align="left" valign="top">&nbsp;</td>
  </tr>
  <tr>
    <td align="left" valign="top">9.1</td>
    <td align="left" valign="top">Your  breach of this Contract or the documents referenced herein;</td>
  </tr>
  <tr>
    <td align="left" valign="top">9.2</td>
    <td align="left" valign="top">Your  violation of any law or the rights of a third party; or</td>
  </tr>
  <tr>
    <td align="left" valign="top">9.3</td>
    <td align="left" valign="top">Your  use of this Site: or</td>
  </tr>  
  <tr>
    <td align="left" valign="top">9.4</td>
    <td align="left" valign="top">Your  violation of any of the rules and regulations of any other bodies referred to  in condition 7.2.3 of these terms and conditions; or</td>
  </tr> 
    <tr>
    <td align="left" valign="top">9.5</td>
    <td align="left" valign="top">any  other activity on Your part which gives rise to such a claim, cause of action,  demands or recovery.</td>
  </tr>
  <tr>
    <td align="left" valign="top">&nbsp;</td>
    <td align="left" valign="top">&nbsp;</td>
  </tr>
  <tr>
    <td colspan="2" align="left" valign="top"><p align="left"><strong>10. PAYMENT POLICY</strong></p></td>
    </tr>
  <tr>
    <td align="left" valign="top">10.1</td>
    <td align="left" valign="top">Whilst  access to the Site is free of charge the use of the Premium Membership  facilities  is not and You will pay Us  for such use at the rate specified on the Site using the secure payment service  We in our absolute discretion from time to time decide</td>
  </tr>  
  <tr>
    <td align="left" valign="top">10.2</td>
    <td align="left" valign="top">The  fee for the Premium Membership does not include any fee payable by the payment  service We use nor any fee charged to You by your Bank or other third parties</td>
  </tr> 
    <tr>
    <td align="left" valign="top">&nbsp;</td>
    <td align="left" valign="top">&nbsp;</td>
  </tr>
  <tr>
    <td colspan="2" align="left" valign="top"><p align="left"><strong>11. PREMIUM MEMBERSHIP</strong></p></td>
    </tr>
  <tr>
    <td align="left" valign="top">11.1</td>
    <td align="left" valign="top">On  payment by You to Us of the Registration Fee and subject to Our accepting You  as such We will award You Premium Membership.</td>
  </tr>
  <tr>
    <td align="left" valign="top">11.2</td>
    <td align="left" valign="top">As  a Premium Member You will receive:</td>
  </tr>  
  <tr>
    <td align="left" valign="top">11.2.1</td>
    <td align="left" valign="top">Payment  1 each time You introduce to Us a new Premium Member (&ldquo;nPM&rdquo;)</td>
  </tr> 
    <tr>
    <td align="left" valign="top">11.2.2</td>
    <td align="left" valign="top">Payment  2 each time the nPM introduces a new Premium Member (&ldquo;nnPM&rdquo;)</td>
  </tr>
  <tr>
    <td align="left" valign="top">11.2.3</td>
    <td align="left" valign="top">Payment  3 each time the nnPM introduces a new Premium Member</td>
  </tr>
  <tr>
    <td align="left" valign="top">&nbsp;</td>
    <td align="left" valign="top">&nbsp;</td>
  </tr>
  <tr>
    <td align="left" valign="top">11.3</td>
    <td align="left" valign="top">Any  person applying to join the Site as a Premium Member will only qualify to join  as a Premium Member if they have not been a Premium Member of the Site for the  period of 6 years preceding the date of their application to become a Premium  Member.</td>
  </tr>  
  <tr>
    <td align="left" valign="top">&nbsp;</td>
    <td align="left" valign="top">&nbsp;</td>
  </tr> 
    <tr>
    <td align="left" valign="top">11.4</td>
    <td align="left" valign="top">No  payment of the accumulated sums due under sub-clauses 11.2.1, 11.2.2 and 11.2.3  will be made to You until the balance of monies in Your account is in excess of  US$100.00.</td>
  </tr>
  <tr>
    <td align="left" valign="top">&nbsp;</td>
    <td align="left" valign="top">&nbsp;</td>
  </tr>
  <tr>
    <td align="left" valign="top">11.5</td>
    <td align="left" valign="top">All  payments to and fromYou will be made only through our PayPal account andYou  will be notified of any payment We make to You through the email address You  have registered with Us</td>
  </tr>
  <tr>
    <td align="left" valign="top">&nbsp;</td>
    <td align="left" valign="top">&nbsp;</td>
  </tr>  
  <tr>
    <td align="left" valign="top">11.6</td>
    <td align="left" valign="top"><p align="left">From all payments made to You We  will deduct such reasonable handling and administration fees as We in our sole  discretion deem appropriate. </p></td>
  </tr> 
    <tr>
    <td align="left" valign="top">&nbsp;</td>
    <td align="left" valign="top">&nbsp;</td>
  </tr>
  <tr>
    <td align="left" valign="top">11.7</td>
    <td align="left" valign="top">As  a Premium Member You will also have access to the following facilities:</td>
  </tr>
  <tr>
    <td align="left" valign="top">11.7.1</td>
    <td align="left" valign="top">You  may post a job on the Site</td>
  </tr>
  <tr>
    <td align="left" valign="top">11.7.2</td>
    <td align="left" valign="top">You  may create a corporate Profile</td>
  </tr>  
  <tr>
    <td align="left" valign="top">11.7.3</td>
    <td align="left" valign="top">You  may view all the statistics We collect as to who has view Your Profile</td>
  </tr> 
    <tr>
    <td align="left" valign="top">&nbsp;</td>
    <td align="left" valign="top">&nbsp;</td>
  </tr>
  <tr>
    <td colspan="2" align="left" valign="top"><p align="left"><strong>12. SOFTWARE</strong></p></td>
    </tr>
  <tr>
    <td colspan="2" align="left" valign="top"><p align="left">If We supply any Software to You,  You agree that it will be used in accordance with the provisions of this clause</p></td>
    </tr>
  <tr>
    <td align="left" valign="top">12.1</td>
    <td align="left" valign="top">You  will pay to Us the sum of US$1.00 if we demand payment from You</td>
  </tr>  
  <tr>
    <td align="left" valign="top">12.2</td>
    <td align="left" valign="top">You  will have the non-exclusive right to use the Software as Our licensee for the  period of 12 months from the date upon which We supply it to you and then for  such additional period as We will notify You from time to time</td>
  </tr> 
    <tr>
    <td align="left" valign="top">12.3</td>
    <td align="left" valign="top">You  may not copy the Software or pass it on to any other person firm or company  without our consent in writing such consent to be on such terms (including fees  and payments) as We in our sole discretion decide.</td>
  </tr>
  <tr>
    <td align="left" valign="top">12.4</td>
    <td align="left" valign="top">You  may not alter or modify the Software in any form and will not decompile or  otherwise seek to reverse engineer it</td>
  </tr>
  <tr>
    <td align="left" valign="top">12.5</td>
    <td align="left" valign="top">You  will use the Software strictly in accordance with the instructions and  procedures, which We supply from time to time.</td>
  </tr>
  <tr>
    <td align="left" valign="top">12.6</td>
    <td align="left" valign="top">You  acknowledge that all rights in the Software belong to Us and/or to those from  whom we have acquired the Software and that You will not act in any way which  might or will prejudice Our ability fully to exploit the Software and all parts  of it.</td>
  </tr>  
  <tr>
    <td align="left" valign="top">12.7</td>
    <td align="left" valign="top">We  may terminate Your right to use the Software at any time on giving You 3 days  notice by email of Our intention to do so and after such Notice You will  immediately cease to use the Software and undertake that it will be removed  immediately after the expiry of the notice from all machines upon which You are  using it</td>
  </tr> 
    <tr>
    <td align="left" valign="top">&nbsp;</td>
    <td align="left" valign="top">&nbsp;</td>
  </tr>
  <tr>
    <td colspan="2" align="left" valign="top"><p align="left"><strong>13. REFUND/CANCELLATION DELIVERY POLICIES</strong></p></td>
    </tr>
  <tr>
    <td align="left" valign="top">13.1</td>
    <td align="left" valign="top">No  refund of the Registration Fee will be permitted:</td>
  </tr>
  <tr>
    <td align="left" valign="top">13.1.1</td>
    <td align="left" valign="top">after  We have accepted You as a Premium Member.</td>
  </tr>  
  <tr>
    <td align="left" valign="top">13.1.2</td>
    <td align="left" valign="top">No  refund will be made for any job package purchased </td>
  </tr> 
    <tr>
    <td align="left" valign="top">13.1.3</td>
    <td align="left" valign="top">No  refund will be made for and Featured Recruiters package selected.</td>
  </tr>
  <tr>
    <td align="left" valign="top">&nbsp;</td>
    <td align="left" valign="top">&nbsp;</td>
  </tr>
  <tr>
    <td align="left" valign="top">13.2</td>
    <td align="left" valign="top">We  will deliver to You Your unique Premium Member number and all other associated  details by email to the address You have registered with Us within 24 hours of  the payment of the Registration Fee</td>
  </tr>
  <tr>
    <td align="left" valign="top">&nbsp;</td>
    <td align="left" valign="top">&nbsp;</td>
  </tr>  
  <tr>
    <td colspan="2" align="left" valign="top"><p align="left"><strong>14. OFFENSIVE CONTENT</strong></p></td>
    </tr> 
    <tr>
    <td align="left" valign="top">&nbsp;</td>
    <td align="left" valign="top">If  You find any Content on the Site in any way offensive, libellous, incorrect or  in any other way inappropriate, You should notify us immediately, using the  contact details shown on the Site and supply any information to validate your  assertion</td>
  </tr>
  <tr>
    <td align="left" valign="top">&nbsp;</td>
    <td align="left" valign="top">If Huntable  finds in, its sole discretion, that any Content notified to it is inappropriate  for the Site, it will take all reasonable steps to remove that Content as soon  as it is able</td>
  </tr>
  <tr>
    <td align="left" valign="top">&nbsp;</td>
    <td align="left" valign="top">&nbsp;</td>
  </tr>
  <tr>
    <td colspan="2" align="left" valign="top"><p align="left"><strong>15. MODIFICATIONS TO THESE TERMS AND CONDITIONS</strong></p></td>
    </tr>  
  <tr>
    <td colspan="2" align="left" valign="top"><p align="left">We reserve the right to modify  these Terms and Conditions from time to time without notifying You. You agree  to revisit these Terms and Conditions from time to time to establish whether or  not any changes have been made and by accepting these Terms and Conditions You  confirm that you have read and understood them on each visit You make to the  Site</p></td>
    </tr> 
    <tr>
    <td align="left" valign="top">&nbsp;</td>
    <td align="left" valign="top">&nbsp;</td>
  </tr>
  <tr>
    <td colspan="2" align="left" valign="top"><p align="left"><strong>16.  MODIFICATIONS TO THE SITE</strong></p></td>
    </tr>
  <tr>
    <td colspan="2" align="left" valign="top"><p align="left">We reserve the right, at Our sole  discretion, to modify, alter or remove any part of the Site or discontinue any  services it provides from time to time and to discontinue the Site completely  and nothing contained on the Site or in these Terms and Conditions shall give Huntable  or the Owner any obligation, of any type, to continue the operation of the Site  or any part of it.</p></td>
    </tr>
  <tr>
    <td align="left" valign="top">&nbsp;</td>
    <td align="left" valign="top">&nbsp;</td>
  </tr>  
  <tr>
    <td colspan="2" align="left" valign="top"><p align="left"><strong>17. GENERAL MATTERS</strong></p></td>
    </tr> 
    <tr>
    <td align="left" valign="top">17.1</td>
    <td align="left" valign="top">Operative  Law – This  Contract will be governed by and construed in accordance with English law and  each of the parties agrees to submit to the exclusive jurisdiction of the  English courts as regards any claim or matter arising under this Contract</td>
  </tr>
  <tr>
    <td align="left" valign="top">17.2</td>
    <td align="left" valign="top">Partnership/Joint  Ventures – no contractual relationship other than that envisaged by these Terms  and Conditions is intended to arise under this contract and You agree that no  relationship of any sort exists between Huntable and/or the Owner and You, save  as provided for herein</td>
  </tr>
  <tr>
    <td align="left" valign="top">17.3</td>
    <td align="left" valign="top">Effect  Of Contract - this Contract supersedes any previous Contract between the  parties in relation to the matters dealt with in them and represents the entire  understanding between the parties.</td>
  </tr>
  <tr>
    <td align="left" valign="top">17.4</td>
    <td align="left" valign="top">Warranties  - all parties acknowledge and agree that they have not entered into this  contract in reliance on any representation, warranty or undertaking which is  not set out or referred to in it</td>
  </tr>  
  <tr>
    <td align="left" valign="top">17.5</td>
    <td align="left" valign="top">Unenforceability  - If  any provision or term of this Contract is, or shall become, or be declared  illegal, invalid or unenforceable for any reason whatsoever (including, but  without limitation, by reason of the provisions of any legislation or other  provisions having the force of law or by reason of any decision of any Court or  other body or authority having jurisdiction over the parties of this Contract)  such terms or provisions shall be divisible from this Contract and shall be  deemed to be deleted from this Contract and the remainder of the provisions  shall continue in full force and effect provided always that if any such  deletion substantially affects or alters the commercial basis of this Contract  the parties shall negotiate in good faith to amend and modify the provisions  and terms of this Contract as necessary or desirable in the circumstances.</td>
  </tr> 
    <tr>
    <td align="left" valign="top">17.6</td>
    <td align="left" valign="top">Notices  - notices by either party must be given in writing and may be delivered  personally or sent by letter (or transmitted by e-mail) addressed to the other  party at its registered office. Any such notice given by letter will be deemed to  have been given at the time at which the letter would be delivered in the  ordinary course of post, if sent by post, and on the date of delivery if  delivered personally (or at the date of receipt if transmitted by e-mail).</td>
  </tr>
  <tr>
    <td align="left" valign="top">17.7</td>
    <td align="left" valign="top">Entire  Agreement - this  Contract constitutes the entire understanding between the parties concerning  the subject matter hereof.</td>
  </tr>
</table>
<p>&nbsp;</p>
</p>
 </div>         










      
      
      
          
                
            
      </div>
      
      	
        </div>
        
      
      
      
    
  </div>
      <!-- content inner ends --> 
    </div>
<!-- content section ends -->
<!-- Range Slider Script Begins -->
<!--<script type="text/javascript" src="js/jquery-1.7.1.min.js"></script>-->
<script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js" type="text/javascript"></script>
<script src="js/jquery.dragval-1.0-pack.js" type="text/javascript"></script>
<script type="text/javascript" src="js/flipcounter.js"></script>
<!-- Range Slider Script Ends -->


<!-- Footer section ends -->

    
    
    <script type="text/javascript" src="js/query.easing.1.3.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        var autoPlayTime = 7000;
        autoPlayTimer = setInterval(autoPlay, autoPlayTime);
        function autoPlay() {
            Slidebox('next');
        }
        $('#slidebox .next').click(function () {
            Slidebox('next', 'stop');
        });
        $('#slidebox .previous').click(function () {
            Slidebox('previous', 'stop');
        });
        var yPosition = ($('#slidebox').height() - $('#slidebox .next').height()) / 2;
        $('#slidebox .next').css('top', yPosition);
        $('#slidebox .previous').css('top', yPosition);
        $('#slidebox .thumbs a:first-child').removeClass('thumb').addClass('selected_thumb');
        $("#slidebox .content").each(function (i) {
            slideboxTotalContent = i * $('#slidebox').width();
            $('#slidebox .container').css("width", slideboxTotalContent + $('#slidebox').width());
        });
    });

    function Slidebox(slideTo, autoPlay) {
        var animSpeed = 1000; //animation speed
        var easeType = 'easeInOutExpo'; //easing type
        var sliderWidth = $('#slidebox').width();
        var leftPosition = $('#slidebox .container').css("left").replace("px", "");
        if (!$("#slidebox .container").is(":animated")) {
            if (slideTo == 'next') { //next
                if (autoPlay == 'stop') {
                    clearInterval(autoPlayTimer);
                }
                if (leftPosition == -slideboxTotalContent) {
                    $('#slidebox .container').animate({ left: 0 }, animSpeed, easeType); //reset
                    $('#slidebox .thumbs a:first-child').removeClass('thumb').addClass('selected_thumb');
                    $('#slidebox .thumbs a:last-child').removeClass('selected_thumb').addClass('thumb');
                } else {
                    $('#slidebox .container').animate({ left: '-=' + sliderWidth }, animSpeed, easeType); //next
                    $('#slidebox .thumbs .selected_thumb').next().removeClass('thumb').addClass('selected_thumb');
                    $('#slidebox .thumbs .selected_thumb').prev().removeClass('selected_thumb').addClass('thumb');
                }
            } else if (slideTo == 'previous') { //previous
                if (autoPlay == 'stop') {
                    clearInterval(autoPlayTimer);
                }
                if (leftPosition == '0') {
                    $('#slidebox .container').animate({ left: '-' + slideboxTotalContent }, animSpeed, easeType); //reset
                    $('#slidebox .thumbs a:last-child').removeClass('thumb').addClass('selected_thumb');
                    $('#slidebox .thumbs a:first-child').removeClass('selected_thumb').addClass('thumb');
                } else {
                    $('#slidebox .container').animate({ left: '+=' + sliderWidth }, animSpeed, easeType); //previous
                    $('#slidebox .thumbs .selected_thumb').prev().removeClass('thumb').addClass('selected_thumb');
                    $('#slidebox .thumbs .selected_thumb').next().removeClass('selected_thumb').addClass('thumb');
                }
            } else {
                var slide2 = (slideTo - 1) * sliderWidth;
                if (leftPosition != -slide2) {
                    clearInterval(autoPlayTimer);
                    $('#slidebox .container').animate({ left: -slide2 }, animSpeed, easeType); //go to number
                    $('#slidebox .thumbs .selected_thumb').removeClass('selected_thumb').addClass('thumb');
                    var selThumb = $('#slidebox .thumbs a').eq((slideTo - 1));
                    selThumb.removeClass('thumb').addClass('selected_thumb');
                }
            }
        }
    }
</script>



    

    
  

</html>

</asp:Content>
