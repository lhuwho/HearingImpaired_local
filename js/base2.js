//負責ajax 傳接json資料

var CHECKBOX_VALUE_SPLIT=';';//the word using to split multi-checkbox's value;

function log(msg)
{
    if( window.console )
    {
        console.log(msg);
    }
}
function Map(){
	this.load_map_edit=function(City,Area,Road,Xaxis,Yaxis,isReload){
		
		//alert("loadMapEdit");
		var cLat = $(Xaxis).html();
		var cLng = $(Yaxis).html();
		var isEmpty=false;
		if(cLat == '0' || cLat == '0.000000' || cLat == '' || cLat == 'null'){
			cLat = '25.044557';
			isEmpty=true;
		}
		if(cLng == '0' || cLng == '0.000000' || cLng == '' || cLng == 'null'){	
			cLng = '121.549910';
			isEmpty=true;
		}
		var latlng = new google.maps.LatLng(cLat,cLng);
		var marker;
		var map = new google.maps.Map($("#map")[0], {
		      zoom: 13,
		      center: latlng,
		      mapTypeId: google.maps.MapTypeId.ROADMAP
		    });
		var geocoder = new google.maps.Geocoder();
		
		var infoWindow = new google.maps.InfoWindow();
		//alert($('#txtCityID :selected').text());
		var checkAddressEmpty=false;
		var checkIndex=$(City+' select').get(0).selectedIndex; 
		if(checkIndex==0)
			checkAddressEmpty=true;
		checkIndex=$(Area+' select').get(0).selectedIndex; 
		if(checkIndex==0)
			checkAddressEmpty=true;
		checkIndex=$(Road+' select').get(0).selectedIndex; 
		if(checkIndex==0)
			checkAddressEmpty=true;
		if(checkAddressEmpty && isEmpty)
		{
			alert("地址或是經緯度其中一個要有值");
			complete_map();
		}
		//(!checkAddressEmpty &&isReload) ||
		else if((!checkAddressEmpty &&isEmpty)||(isReload &&!checkAddressEmpty ))
		{
			
			if (geocoder) {
				var address = $(City+' :selected').text()+$(Area+' :selected').text()+
				$(Road+' :selected').text();
			    geocoder.geocode( { 'address': address}, function(results, status) {
			    	  
			        if (status == google.maps.GeocoderStatus.OK) {
			        	//alert('hi');
			        	//alert(results[0].geometry.location);
				        map.setCenter(results[0].geometry.location);
				        marker = new google.maps.Marker({
				  	      map: map,
				  	      position: results[0].geometry.location,
				  	      draggable: true,
				  	      title: '拖曳我以設定位置'
				        });
					  var MapX = Math.round(marker.getPosition().lat()*1000000)/1000000;
					  var MapY = Math.round(marker.getPosition().lng()*1000000)/1000000;
					  $(Xaxis).html(MapX);
					  $(Yaxis).html(MapY);
					  //alert(marker.getPosition().lat());
					  content = address + "<br/><button type='button' onclick='complete_map();'>完成定位</button> | <button type='button' onclick='reload_map();'>重新定位</button> ";
					  infoWindow.setContent(content);
					  infoWindow.open(map,marker);
					  google.maps.event.addListener(marker, 'mouseup', MoveListener);
			        } else {
			          alert("Geocode was not successful for the following reason: " + status);
			        }
			      });
			 }
		}
		else
		{
			geocoder.geocode( { 'latLng': latlng}, function(results, status) {
		        if (status == google.maps.GeocoderStatus.OK) {
				  if(results[0]){
					  marker = new google.maps.Marker({
					      map: map,
					      position: latlng,
					      draggable: true,
					      title: '拖曳我以設定位置'
					 });
						  var content = results[0].formatted_address;
						  var content2 = results[2].formatted_address;
		                                  content = content + "<br/><button type='button' onclick='complete_map();'>完成定位</button> |  <button type='button' onclick='reload_map();'>重新定位</button> ";
						  infoWindow.setContent(content);
						  infoWindow.open(map,marker);
						  var MapX = Math.round(marker.getPosition().lat()*1000000)/1000000;
						  var MapY = Math.round(marker.getPosition().lng()*1000000)/1000000;
						  $(Xaxis).html(MapX);
						  $(Yaxis).html(MapY);
						  google.maps.event.addListener(marker, 'mouseup', MoveListener);
					  }
			        } else {
			          alert("Geocode failed due to: " + status);
			        }
			});
		}
		//return;

		function MoveListener(){
			

	    	infoWindow.close();
			
	    	var Lat2 = marker.getPosition().lat();
			var Lng2 = marker.getPosition().lng();
			var latlng2 = new google.maps.LatLng(Lat2,Lng2);
				geocoder.geocode( { 'latLng': latlng2}, function(results, status) {
			        if (status == google.maps.GeocoderStatus.OK) {
					  if(results[0]){
						  var content = results[0].formatted_address;
						  var content2 = results[2].formatted_address;
	                      content = content + 
	                      "<br/><button type='button' onclick='complete_map();'>完成定位</button> | <button type='button' onclick='reload_map();'>重新定位</button> ";
						  infoWindow.setContent(content);
						  infoWindow.open(map,marker);
						  var MapX = Math.round(marker.getPosition().lat()*1000000)/1000000;
						  var MapY = Math.round(marker.getPosition().lng()*1000000)/1000000;
						  //alert(MapX);
						  $(Xaxis).html(MapX);
						  $(Yaxis).html(MapY);
					  }
			        } else {
			          alert("Geocode failed due to: " + status);
			        }
			      });
			      
		}
		
		
	      
	}
	this.load_map=function(){
		//alert("loadMap");
		var cLat = $('#txtMapXaxis').html();
		var cLng = $('#txtMapYaxis').html();
		if(cLat == '0' || cLat == '' || cLat == 'null'){
			cLat = '25.053000';
		}
		if(cLng == '0' || cLng == '' || cLng == 'null'){	
			cLng = '121.524000';
		}
		var latlng = new google.maps.LatLng(cLat,cLng);
		var marker;
		var map = new google.maps.Map($("#map")[0], {
		      zoom: 13,
		      center: latlng,
		      mapTypeId: google.maps.MapTypeId.ROADMAP
		    });
		var infoWindow = new google.maps.InfoWindow();
		marker = new google.maps.Marker({
		      map: map,
		      position: latlng,
		      draggable: false
		});
		var geocoder = new google.maps.Geocoder();
		geocoder.geocode( { 'latLng': latlng}, function(results, status) {
	        if (status == google.maps.GeocoderStatus.OK) {
			  if(results[0]){
					  var content = results[0].formatted_address;
					  var content2 = results[2].formatted_address;
					  infoWindow.setContent(content);
					  infoWindow.open(map,marker);
					  var MapX = Math.round(marker.getPosition().lat()*1000000)/1000000;
					  var MapY = Math.round(marker.getPosition().lng()*1000000)/1000000;
					  $('#txtMapXaxis').html(MapX);
					  $('#txtMapYaxis').html(MapY);
				  }
		        } else {
		          alert("Geocode failed due to: " + status);
		        }
		});
		//alert($('#txtCityID :selected').text());
		if($('#txtCityID').html()!='' && $('#txtAreaID').html()!='' && $('#txtRoadID').html()!=''){
			var address = $('#txtCityID').html()+$('#txtAreaID').html()+$('#txtRoadID').html();
			//alert(address);
			if (geocoder) {
			      geocoder.geocode( { 'address': address}, function(results, status) {
			        if (status == google.maps.GeocoderStatus.OK) {
					  map.setCenter(results[0].geometry.location);
					  marker = new google.maps.Marker({
					      map: map,
					      position: results[0].geometry.location,
					      draggable: false,
					      title: '拖曳我以設定位置'
					    });
					  var MapX = Math.round(marker.getPosition().lat()*1000000)/1000000;
					  var MapY = Math.round(marker.getPosition().lng()*1000000)/1000000;
					  $('#txtMapXaxis').html(MapX);
					  $('#txtMapYaxis').html(MapY);
					  //alert(marker.getPosition().lat());
					  infoWindow.setContent(address);
					  infoWindow.open(map,marker);
			        } else {
			          //alert("Geocode was not successful for the following reason: " + status);
			        }
			      });
			 }
		}
	}
	
}
function Permission(iSetting){
	var Setting=iSetting;
}
function Check(){
	this.IsServerFileExist=function(filePath,successCallback){
		$.ajax({
		    url:filePath,
		    type:'HEAD',
		    success:function(){
		    	successCallback(filePath);
		    },
		    error:function(){
		        
		    }
		});
		
	}
	this.chkStaffCodeForm = function(txtStaffCode,callback){
		if(txtStaffCode==undefined || txtStaffCode.length==0)
			return true;
		//   /^[A-Z]\d{9}$/
		reg = /^[0-9]{10}$/;
		  if (reg.test(txtStaffCode)) {
		      return true;
		  }else{
		      return false;
		  }
	}
	this.chkStaffCode=function(txtStaffCode,callback){
		var dataStr = {code: "Login", actionID: "chkStaffCode", txtStaffCode:txtStaffCode};
		$.ajax({
			type: "POST",
			url: "action.php",
			data: dataStr,
			success: function(out) {
				//(result[item].src == null || result[item].src.length < 1)
			if(!(out == null || out.length == 0 || out==undefined))
			{
				var myObject = eval('(' + out + ')');
							//alert(out);
							if(myObject["txtIsExist"]==0)
								callback(false);//確認沒有重複
							else
								callback(true);
				}
			}
		});
	}
	this.chkBranchCode=function(txtBranchCode,callback){
		var dataStr = {code: "Branch", actionID: "chkBrnachCode", txtBranchCode:txtBranchCode};
		$.ajax({
			type: "POST",
			url: "action.php",
			data: dataStr,
			success: function(out) {
				//(result[item].src == null || result[item].src.length < 1)
			if(!(out == null || out.length == 0 || out==undefined))
			{
				var myObject = eval('(' + out + ')');
							if(myObject["txtIsExist"]==0)
								callback(false);//確認沒有重複
							else
								callback(true);
							
				}
			}
		});
	}
	this.getStaffCode=function(txtBranchCode,callback){
		
		var dataStr = {code: "Login", actionID: "genCode", txtBranchCode:txtBranchCode};
		$.ajax({
			type: "POST",
			url: "action.php",
			data: dataStr,
			success: function(out) {
				//alert(out);
				//(result[item].src == null || result[item].src.length < 1)
				if(!(out == null || out.length == 0 || out==undefined))
				{
					var myObject = eval('(' + out + ')');
					callback(myObject["txtStaffCode"]);
				}
			}
		});
	}
	this.checkLength=function(words,length){
		if(words==undefined || words.length==0)
			return true;
		
		  if (words.length==length) {
		      return true;
		  }else{
		      return false;
		  }
	}
	this.checkCellPhone=function(phone,callback){
		if(phone==undefined || phone.length==0)
			return true;
		//   /^[A-Z]\d{9}$/
		reg = /^[0-9]{10}$/;
		  if (reg.test(phone)) {
			  return true;
		  }else{
			  return false;
		  }
	}
	this.checkHomePhone=function(phone){
		
		if(phone==undefined || phone.length==0)
			return true;
		//   /^[A-Z]\d{9}$/
		reg = /^[0]{1}[0-9]{1,2}\-[0-9]{7,8}$/;
		  if (reg.test(phone)) {
		      return true;
		  }else{
		      return false;
		  }
	}
	this.checkEmail=function(email){
		if(email==undefined || email.length==0)
			return true;
		reg = /^[^\s]+@[^\s]+\.[^\s]{2,3}$/;
		  if (reg.test(email)) {
		      return true;
		  }else{
		      return false;
		  }
	}
	this.HasNoEmptyCheck=function(checkArray,obj,show){
    	if(show==undefined)
    		return;
        var allEmpty=true;
        var emptyString="";
        for(var i=0;i<checkArray.length;i++)
        {
            if(obj[checkArray[i]]!=undefined && obj[checkArray[i]].length!=0)
            {   
                allEmpty=false;
            }
        }
        if(allEmpty)
        {
        	for(var i=0;i<show.length;i++){
        		emptyString+= show[i]+"、";
        	}   
        	emptyString=emptyString.substring(0,emptyString.length-1);
            emptyString+=' 請擇一填寫！';
        }
        
        return emptyString;
    }//emptyString+=$("#"+checkArray[i]).siblings("td").html()+"<br/>";
	this.checkID=function( id ) {
		//alert(id);
		if(id==undefined || id.length==0)
			return true;
		id=id.toUpperCase();
		
		tab = "ABCDEFGHJKLMNPQRSTUVXYWZIO"                     
		A1 = new Array (1,1,1,1,1,1,1,1,1,1,2,2,2,2,2,2,2,2,2,2,3,3,3,3,3,3 );
		A2 = new Array (0,1,2,3,4,5,6,7,8,9,0,1,2,3,4,5,6,7,8,9,0,1,2,3,4,5 );
		Mx = new Array (9,8,7,6,5,4,3,2,1,1);
		
		if ( id.length != 10 ) return false;
		i = tab.indexOf( id.charAt(0) );
		if ( i == -1 ) return false;
		sum = A1[i] + A2[i]*9;
		
		for ( i=1; i<10; i++ ) {
		  v = parseInt( id.charAt(i) );
		  if ( isNaN(v) ) return false;
		  sum = sum + v * Mx[i];
		}
		if ( sum % 10 != 0 ) return false;
		return true;
	}
	this.ReduplicateIDCheck = function(id,callback){
		var dataStr = {code: "Login", actionID: "chkID", txtID:id};
		$.ajax({
			type: "POST",
			url: "action.php",
			data: dataStr,
			success: function(out) {
				//alert(out);
				//(result[item].src == null || result[item].src.length < 1)
				if(!(out == null || out.length == 0 || out==undefined))
				{
					//alert(out);
					var myObject = eval('(' + out + ')');
					callback(myObject["ID_EXIST"]);
				}
			}
		});
	}
}
function Base(){
	var PermissionObj=new Object();
	var HeadSettingSuccessCallback=function(){};
	this.HeadSettingSuccessCallbackSetting=function(callback){
		HeadSettingSuccessCallback=null;
		HeadSettingSuccessCallback=callback;
    }
	this.getQuery =function(name) {
	    var AllVars = window.location.search.substring(1);
	    var Vars = AllVars.split("&");
	    for (i = 0; i < Vars.length; i++) {
	        var Var = Vars[i].split("=");
	        if (Var[0] == name) return Var[1];
	    }
	    return "";
	};
    this.getStaffList=function(callback){
        var dataStr = {code: "List", actionID: "getStaffList"};
        $.ajax({
            type: "POST",
            url: "action.php",
            data: dataStr,
            success:callback
        });
    }
    
    this.getStaffID=function(StaffCode,callback){
    	if(StaffCode==undefined){
    		callback("");
    	}
    	else{
        var dataStr = {code: "List", actionID: "getStaffID", txtStaffCode: StaffCode};
        $.ajax({
            type: "POST",
            url: "action.php",
            data: dataStr,
            success:callback
        });
    	}
    }
    this.getCaseID=function(CaseCode,callback){
        var dataStr = {code: "Case", actionID: "searchCaseIDByCode", txtCaseCode: CaseCode};
        $.ajax({
            type: "POST",
            url: "action.php",
            data: dataStr,
            success:function(out){
            	
                var myObject = eval('(' + out + ')');
                
                var CaseID=myObject["txtCaseID"];
                callback(CaseID);
            }
        });
    }
    this.getShopID=function(ShopCode,callback){
        var dataStr = {code: "Community", actionID: "searchShop", txtShopCode: ShopCode};
        $.ajax({
            type: "POST",
            url: "action.php",
            data: dataStr,
            success:function(out){
                var myObject = eval('(' + out + ')');
                if(myObject[0]==undefined)
                	callback();
                else
                	callback(myObject[0]["txtShopID"]);
            }
        });
    }
    this.getCommID=function(CommCode,callback){
        var dataStr = {code: "Community", actionID: "searchCommunity", txtCommCode: CommCode};
        
        $.ajax({
            type: "POST",
            url: "action.php",
            data: dataStr,
            success:function(out){
                var myObject = eval('(' + out + ')');
                if(myObject[0]==undefined)
                	callback();
                else
                	callback(myObject[0]["txtCommID"]);
                
            }
        });
    }
    this.getBranchID=function(BranchCode,callback){
        var dataStr = {code: "Branch", actionID: "searchBranchSimplified", txtBranchCode: BranchCode};
        //"",""
        $.ajax({
            type: "POST",
            url: "action.php",
            data: dataStr,
            success:function(out){
                //alert(out);
                var myObject = eval('(' + out + ')');
                var BranchID=myObject[0]["txtBranchID"];
                callback(BranchID);
            }
        });
    }
    this.getGroupID=function(GroupCode,callback){
        //alert(GroupCode);
        var dataStr = {code: "ACL", actionID: "searchGroup", txtGroupCode: GroupCode};
        
        $.ajax({
            type: "POST",
            url: "action.php",
            data: dataStr,
            success:function(out){
                //alert(out);
                var myObject = eval('(' + out + ')');
                var GroupID=myObject[0]["txtGroupID"];
                //alert("groupid"+GroupID);
                callback(GroupID);
            }
        });
    }
    this.getStaffIDbyName=function(StaffCode,callback){
        var dataStr = {code: "List", actionID: "getStaffID", txtStaffName: StaffCode};
        
        $.ajax({
            type: "POST",
            url: "action.php",
            data: dataStr,
            success:callback
        });
    }
    this.getUploadData=function(tableName){
        var obj = {};
        $("#"+tableName+" table td").each(function (n){
        	var temp=$(this).html();
            if($(this).attr("id").length>0){
            obj[$(this).attr("id")]=$(this).html();
            }
        });
        
        $("#"+tableName+" table td span").each(function (n){
            if($(this).attr("id").length>0){
            obj[$(this).attr("id")]=$(this).html();
            }
        });        
        return obj;
    }
    //將欄位資料parse出來轉成json
    //if compareObj!=null 會比較差異的部分才抓出來
    //if compareObj==null 不比較差異的部分 通通抓出來
    this.getTextValueBase= function(htmltableName,compareObj){
        var obj = {};
        if(htmltableName.length==0)
        {
        	return obj;
        }
        
        
    	//$("#"+htmltableName+" .tableDiv div").each(function(n){ 拿掉div 因為本來設定是沒有要div
    	$("#"+htmltableName+" .tableDiv").each(function(n){
    		//var fn = $(this).attr("name");
    		var val;//=$(this).html();
    		if($(this).children("div").size()==0){
    			val=$(this).html();
    		}
    		else{
    			val=$(this).children("div").html();
    		}
    		var fn = $(this).attr("id");
    		
    		
    		obj[fn] = val;
    	})
        $("#"+htmltableName+" input").add("#"+htmltableName+" textarea").each(function (n)
        {
            //alert(this.type);
            //if(htmltableName.indexOf("subtabs")!=-1)
            //  alert(this.id+"  "+this.type );
            
                var fn ;//= this.id;
                if(this.name.length==0)
                    fn = this.id.replace("gosrh","txt");
                else
                    fn = this.name.replace("gosrh","txt");
                
                //alert(fn);
                var val = ""; //Avoid IE8 JSON bug
                if (this.type == "checkbox" || this.type == "radio")
                    val = this.checked+"";
                else if (this.type == "select-one"){
                    val =$(this).find('option:selected').attr("value")+"";
                    //val = this.options;//[this.selectedIndex].value;
                   // alert(fn+" : "+  val);
                    }else if (this.type == "select-multiple") {
                    var selected = [];
                    $(this).children().each(function(i) {
                        if (this.selected) selected.push(i);
                    });
                    val = selected.join(",")+"";//L_NOTE: , is split
                }
                else{
                    
                    val = this.value + "";
                    //alert(val);
                }
                //alert(fn+"   "+this.id);
               // if(val.length>0)
                //if(htmltableName=="tabs-2")
                //alert(this.type+"  "+fn+"  "+ val);
                var npfn=fn.substring(fn.indexOf("txt"),fn.length);
                if(val.length>0 && val!=undefined && val!="undefined" && val!="請選擇" 
                    && npfn.length>0)
                {   
                    //alert(fn+" "+val);
                    //if(htmltableName=="tabs-2")
                    //  alert(this.type+"  "+this.value);
                    if(fn!="rp")//L_NOTE: what is "rp"?? report pagesize?
                        obj[fn] = val;
                
                }
                else if(compareObj!=undefined && compareObj[fn]!=null && compareObj[fn].length>0){//原本有值 但現在被清空
                	obj[fn] = "";
                }
                /*
                else if(fn.length>0)
                {
                	obj[fn] = "";
                }
                */
            
            //else 
                //alert(this.type);
        });
        $("#"+htmltableName + " select").each(function (n)
        {
                    //alert(this.type);
            if(this.type=="select-one")
            {
                var fn ;//= this.id;
                if(this.id.length==0)
                    fn = this.name.replace("gosrh","txt");
                else{
                    if($(this).attr("class")=="hasDatepicker")
                    {
                        //alert(this.name);
                        fn=this.name.replace("gosrh","txt");
                    }
                    else
                        fn = this.id.replace("gosrh","txt");
                }
                
                var val = ""; //Avoid IE8 JSON bug
                if (this.type == "checkbox" || this.type == "radio")
                    val = this.checked+"";
                else if (this.type == "select-one"){
                	if($(this).attr("id").indexOf("gosrh")!=-1)
                		
                		val=$(this).find('option:selected').text()+"";
                	else
                		val =$(this).find('option:selected').attr("value")+"";
                    //val = this.options;//[this.selectedIndex].value;
                   // alert(fn+" : "+  val);
                }
                else if (this.type == "select-multiple") {
                    var selected = [];
                    $(this).children().each(function(i) {
                        if (this.selected) selected.push(i);
                    });
                    val = selected.join(",")+"";
                }
                else{
                    
                    val = this.value + "";
                    //alert(val);
                }
                //alert(fn+"   "+this.id);
               // if(val.length>0)
                //if(htmltableName=="tabs-2")
                //alert(this.type+"  "+fn+"  "+ val);
                if(val.length>0 && val!=undefined && val!="undefined" && val!="請選擇" 
                    && fn.length>0)
                {   
                    //alert(fn+" "+val);
                    //if(htmltableName=="tabs-2")
                    //  alert(this.type+"  "+this.value);
                    if(fn!="rp")
                        obj[fn] = val;
                
                }
                else if(compareObj!=undefined && compareObj[fn]!=null && compareObj[fn].length>0){//原本有值 但現在被清空
                	obj[fn] = "";
                }
                if(fn.indexOf("txtRoadID")!=-1 || fn.indexOf("txtAreaID")!=-1){
                	obj[fn.replace("ID","")]=$(this).find('option:selected').text()+"";
                }
            }
            //else 
                //alert(this.type);
        });
    
        
        return obj;
    }
  /**
   * get input values (ready for do back-end action!) 
   *@param
   *@param
   *@param HTML_ID, if <div id="abc"> then HTML_ID='abc'
   *@see getTextValueBase
   *@return 
   */
    //if compareObj!=null 會比較差異的部分才抓出來
    //if compareObj==null 不比較差異的部分 通通抓出來
    this.getTextValue= function(code,actionID,htmltableName,compareObj){
        var obj = this.getTextValueBase(htmltableName,compareObj);
        obj["code"] = code;//add more item for obj
        obj["actionID"] = actionID;//add more item for obj
        //alert(htmltableName);
        
        return obj;
    };
    //找尋list資料
    this.getOtherListBase = function(callback ,code,actionID){
        var dataStr = {code: code, actionID: actionID};
        $.ajax({
            type: "POST",
            url: "action.php",//"list.php",
           // async:false,
            data: dataStr,
            success: callback
        });
    };
    //外部傳一個tbale名字先parse資料後在傳給後台
    this.DataBaseConnect = function(code,actionID,tableName,Callback,ID){
        var obj=this.getTextValue(code,actionID,tableName);
        for( key in ID )
        {
            //alert(key+"  "+ID[key]);
            if(ID[key]>=0)
                obj[key]=ID[key];
            //alert(key +"  "+ID[key]);
        }

        
        $.ajax({
            type: "POST",
            url: "action.php",
            data: obj,
            success: Callback
        });
    };
    //for少數特例 像是head_set.js傳遞主管陣列就無法用DataBaseConnect/////
    //只能先在外面處理過後再丟進來/////////////////
    
    //作者:Aaron
    //註解者:shanhu
    //obj:code:"ACL",actionID:"getAccRights"
    //callback:連結"action.php"取得out，利用AJAX傳接JSON資料
    //日期:2011/08/17
    //描述:跟後台拿取資料
    this.DataBaseConnectUsingObj=function(obj,Callback){
        $.ajax({
            type: "POST",
            url: "action.php",
            data: obj,
            success: Callback
        });
    }
    this.getCurrentDate=function(){
        var d=new Date();
        var month=(d.getMonth()+1)+"";
        if(month.length==1)
            month="0"+month;
        var day=d.getDate()+"";
        if(day.length==1)
            day="0"+day;
        return d.getFullYear()+'/'+month+'/'+ day;//2011/03/29
    }
    this.Delete = function(callback,code,actionID,dataStr){
        //var dataStr = {code: code, actionID: actionID,DELETED:1};
        dataStr["code"]=code;
        dataStr["actionID"]=actionID;
        dataStr["txtDELETED"]=1;
        $.ajax({
            type: "POST",
            url: "action.php",//"list.php",
            data: dataStr,
            success: callback
        });
    }
    this.getGroup = function(callback){
        this.getOtherListBase(callback,"ACL","searchGroup");
    }
    this.noEmptyCheck=function(checkArray,obj,ListObj,showWordArray){
        var returncheck='未填寫：';//'<div id="popupShowMessage">';
        var counter=0;
        for(var i=0;i<checkArray.length;i++)
        {
            //alert( checkArray[i] +"  "+obj[checkArray[i]]);
            //alert($("#txtContDataSourceID ").html());
            //$("#"+checkArray[i]).siblings("td").html();
            //$("#"+checkArray[i]+" ~ td").attr("width");
            var tempstring=checkArray[i];
            var className=$('#'+tempstring).attr('class');
            if(className==undefined){}
            else if(className.indexOf('tableList')!=-1){
                
                var key=tempstring.substring(tempstring.indexOf("txt"),tempstring.length);
                if(tempstring.indexOf("City")!=-1  ){
                    var item = $('select[name='+tempstring+']'+' option:selected').text();
                    
                    if(item=='縣市' || item.length==0){
                        returncheck+="縣市、";//<br/>";
                        counter++;
                    }
                }
                else if(tempstring.indexOf("Area")!=-1){
                    var item = $('select[name='+tempstring+']'+' option:selected').text();
                    if(item=='區域' || item.length==0){
                        returncheck+="鄉鎮市區、";//<br/>";
                        counter++;
                    }
                    
                }
                else if($("select[name="+tempstring+"]").attr('value')=='請選擇'){
                	returncheck+=showWordArray[i]+"、";//<br/>";
                	counter++;
                }
                /*
                else if(ListObj!=undefined){
                    if(ListObj[key]!=undefined){
                        if(ListObj[key][obj[tempstring]]=="請選擇"){
                        	returncheck+=showWordArray[i]+"、";//<br/>";
                        	// returncheck+=$("#"+tempstring).prev().html()+" ";//<br/>";
                            counter++;
                            //alert(checkArray[i]);
                            //alert($("#"+checkArray[i]).attr("class"));
                        }
                    }
                }
                */
            }
            else{
            	var checkString=obj[tempstring];
                if(checkString=="-1"||checkString==undefined || checkString.length==0)
                {   
                	returncheck+=showWordArray[i]+"、";//<br/>";
                   // returncheck+=$("#"+checkArray[i]).prev().html()+"未填寫<br/>";
                    counter++;
                //alert( checkArray[i] +"  "+obj[checkArray[i]]);
                }
            }
            
            //if(obj[checkArray[i]].length==0)
            //  returncheck+=$("#"+checkArray[i]).siblings("td").html()+"未填寫<br/>";
        }
        returncheck+='';
        //alert(counter+"  "+returncheck);
        if(counter==0)
            returncheck="";
        
        return returncheck;
    }
    
    this.AddressNoEmptyCheck=function(checkArray,obj){
        var allEmpty=true;
        var emptyString="";
        for(var i=0;i<checkArray.length;i++)
        {
            if(obj[checkArray[i]]!=undefined && obj[checkArray[i]].length!=0)
            {   
                allEmpty=false;
            }
        }
        if(allEmpty)
        {
            emptyString="";
                //$("#"+checkArray[i]).prev().html()+"未填寫<br/>";
            //alert(checkArray[0]);
                //emptyString+=$("#"+checkArray[0]).parent().parent().prev().html()+"<br/>";
            emptyString+="地址未填寫<br/>";
        }
        return emptyString;
    }
	
    //作者:Aaron
    //註解者:shanhu
    //callback:取得out的值，接JSON資料
    //setting:no
    //日期:2011/08/17
    //描述:跟後台要權限資料
    this.getModuleRightsByUser=function(callback,setting){
    	/*
    	$(".table_box3_display tr td").each(function(){
    		if($(this).attr("class").length==0)
    			$(this).addClass("borderleft");
    		else
    			$(this).addClass("borderRight");
    	});
    	*/
    	
    	//$(".table_box3_display tr").children("td:even").addClass("borderleft");
    	//$(".table_box3_display tr").children("td:odd").addClass("borderRight");
    	$(".border_5").each(function(){
    		//alert($(this).width());
    		$(this).css("width",$(this).width()-20);
    		//alert($(this).width());
    	});
    	if(setting!=undefined){
    		PermissionObj["insertButton"]=setting[0];
    		PermissionObj["UpdateButton"]=setting[1];
    		PermissionObj["ReviewButton"]=setting[2];
    		PermissionObj["DeleteButton"]=setting[3];
    		PermissionObj["SaveButton"]=setting[4];
    	}
    	else{
    		
    		PermissionObj["insertButton"]="bInsert";
    		PermissionObj["UpdateButton"]="bUpdate";
    		PermissionObj["ReviewButton"]="bSearch";
    		PermissionObj["DeleteButton"]="bDelete";
    		PermissionObj["SaveButton"]="bSave";
    	}
        $(".table_box3_display tr td:even").not(".table_box3_display table td").addClass("borderleft");
        $(".table_box3_display tr td:odd").not(".table_box3_display table td").addClass("borderRight");
        $(":text").addClass("def");
        var dataStr = {code: "ACL", actionID: "getUserRights"};
        this.DataBaseConnectUsingObj(dataStr,this.CreateMenu);
        
    }
    
    //作者:Aaron
    //註解者:shanhu
    //日期:2011/09/01
    //描述:建立選單
    this.CreateMenu=function(out){
    	//alert(PermissionObj["insertButton"]);
        this.menuEventSet=function()
        {
            $("ul.topnav li a").bind("mouseover",function() { //When trigger is clicked...
                //Following events are applied to the subnav itself (moving subnav up and down)
                $(this).parent().find("ul.subnav").show(); //Drop down the subnav on click
                $(this).parent().hover(function() {
                    
                }, function(){
                    $(this).parent().find("ul.subnav").hide();//.slideUp('fast'); //When the mouse hovers out of the subnav, move it back up
                });
                //Following events are applied to the trigger (Hover events for the trigger)
                }).hover(function() {
                    
                    $(this).addClass("subhover"); //On hover over, add class "subhover"
                }, function(){  //On Hover Out
                    $(this).removeClass("subhover"); //On hover out, remove class "subhover"
            });
            $(".mainmenu").bind("mouseover",function(){
                 var mysrc=$(this).attr("src").replace("menu_","menu_change_");
                 $(this).attr("src",mysrc);
            }).bind("mouseout",function(){
                var mysrc=$(this).attr("src").replace("menu_change_","menu_");
                $(this).attr("src",mysrc);
            });
        }
        this.GetCreateMenuImg= function(index)
        {
            var path = document.location.pathname;
            var dir = path.substring(0, path.lastIndexOf('/'));
            //alert(path+"  "+dir);
            return '<a href="#"><img class="mainmenu" src="'+dir+'/themes/lena/images/menu/menu_1'+(index+1)+'.jpg"  /></a>';
        }
        function MenuStruct(MainMenu,SubMenu,Src)
        {
            this.MainMenu=MainMenu;
            this.SubMenu=SubMenu;
            this.Src=Src;
        }
        function ModulePermissionCheck(actionList,FileName,subModuleFile,subModuleName){
            var check=false;
           
            for(var j=0;j<actionList.length;j++)
            {
                if(parseInt(actionList[j].New)>=0){
                    check=true;
                }
                else if(parseInt(actionList[j].Edit)>=0){
                    check=true;
                }
                else if(parseInt(actionList[j].Delete)>=0){
                    check=true;
                }
                else if(parseInt(actionList[j].View)>=0){
                    check=true;
                }
            }
            if(FileName==subModuleFile){
            	var disableEditNewCount=0;
            	for(var j=0;j<actionList.length;j++)
                {
                    if(parseInt(actionList[j].New)<0){
                    	disableEditNewCount++;
                        $("."+PermissionObj["insertButton"]).attr('disabled', 'disabled').removeClass("btn_del").html("");
                    }
                    else if(parseInt(actionList[j].Edit)<0){
                    	disableEditNewCount++;
                    	$("."+PermissionObj["UpdateButton"]).attr('disabled', 'disabled').removeClass("btn_del").html("");
                    }
                    else if(parseInt(actionList[j].Delete)<0){
                    	$("."+PermissionObj["DeleteButton"]).attr('disabled', 'disabled').removeClass("btn_del").html("");
                    }
                    else if(parseInt(actionList[j].View)<0){
                    	$("."+PermissionObj["ReviewButton"]).attr('disabled', 'disabled').removeClass("btn_del").html("");
                    }
                    
                }
            	if(disableEditNewCount==2){
                	$("."+PermissionObj["SaveButton"]).attr('disabled', 'disabled').removeClass("btn_del").html("");
            	}
            	//$("title").html(subModuleName);
            	document.title=subModuleName;
            }
            return check;
        }
        this.GetMenuInforation = function(myObject)
        {
            var menu= new Array();
            var MainMenuName= new Array();
            for(var i=0;i<myObject.length;i++)
            {
                var counter=0;
                var checkMainMenuName=0;
                while(counter<MainMenuName.length&&checkMainMenuName==0)
                {
                    if(myObject[i].txtMainModuleName==MainMenuName[counter])
                        checkMainMenuName=1;
                    counter++;
                }
                if(checkMainMenuName==0)
                    MainMenuName[MainMenuName.length]=myObject[i].txtMainModuleName;
            }
            for(var i=0;i<MainMenuName.length;i++)
            {
                menu[i]= new Array();
            }
            var path = document.location.pathname;
            var FileName ="."+ path.substring(path.lastIndexOf('/'),path.length );
            for(var i=0;i<myObject.length;i++){
                var index=0;
                var find=0;
                var actionList= myObject[i].txtActionList;
                if(ModulePermissionCheck(actionList,FileName,
                		myObject[i]["txtSubModuleFile"],myObject[i]["txtSubModuleName"]))
                {
                    while(index<MainMenuName.length && find==0)
                    {
                        if(MainMenuName[index]==myObject[i].txtMainModuleName)
                            find=1;
                        else
                            index++;
                    }
                    menu[index][menu[index].length]=new MenuStruct(myObject[i].txtMainModuleName,
                            myObject[i].txtSubModuleName,myObject[i].txtSubModuleFile);
                }
            }
            return menu;    
        }
        if(!(out == null || out.length == 0 || out==undefined))
        {
            var myObject = eval('(' + out + ')');
            var MenuStruct=this.GetMenuInforation(myObject);
            var innerString="" ;
            
            for(var i=0;i<MenuStruct.length-1;i++)
            {
                if(typeof(MenuStruct[i][0]) != 'undefined')
                {
                    innerString+='<li>';
                        innerString+='<a href="#">'+MenuStruct[i][0].MainMenu+
                        '</a>';//this.GetCreateMenuImg(i);
                        
                        innerString+= '<ul class="subnav li_cont1">';
                        for(var j=0;j<MenuStruct[i].length;j++)
                        {
                            innerString+= '<li><a href="'+MenuStruct[i][j].Src+'">'+
                            MenuStruct[i][j].SubMenu+'</a></li>';
                        }
                        innerString+= '</ul>'+                  
                    '</li>';        
                        innerString+='<span class="floatleft">|</span>';
                }
                
            }
            document.getElementById("topnav").innerHTML=innerString;
            document.cookie = "topnav="+innerString;
            //alert(innerString.length);
            this.menuEventSet();
        }
        var title=$("TITLE").html();
        var inner='<span>'+title+'</span>'+
            '<div><a href="./home.php">首頁</a> / <a href="#">'+title+'</a></div>';
        $("#path_link").html(inner);
        inner='<a class="logo" href="./home.php">台灣房屋</a>'+
        	'<ul id="header_sub_btn">'+
		    	'<li><a href="./buyer_data.php">新增買方</a></li>'+
		        '<span class="floatleft">|</span>'+
		        '<li><a href="./seller_data.php">新增賣方</a></li>'+
		        '<span class="floatleft">|</span>'+
		        '<li><a href="#" style="display:none;">成交行情</a></li>'+
		        '<span class="floatleft" style="display:none;">|</span>'+
		        '<li><a href="#" style="display:none;">稅費試算</a></li>'+
		        '<span class="floatleft" style="display:none;">|</span>'+
		        '<li><a href="#" style="display:none;">操作說明</a></li>'+
		        '<span class="floatleft" style="display:none;">|</span>'+
		        '<li><a class="btn_y_s1" href="#" style="display:none;">物品申請</a></li>'+
		        '<li><a class="btn_y_s1" href="#">教育訓練</a></li>'+
        	'</ul>';
        $("#topbanner").html(inner);
        document.cookie = "topbanner="+inner;
        var date = new Date();
       
        
        var obj={code:"Login", actionID:"getUserData"};
        //作者:Aaron
        //註解者:shanhu
        //obj:{code:,actionID:}
        //callback:連結"action.php"取得out，利用AJAX傳接JSON資料
        //日期:2011/09/01
        //描述:資料庫連結使用的物件
    	cMyBase.DataBaseConnectUsingObj(obj,function(UserDataOut){
    		var StaffCode="";
			var StaffName="";	
    		if(!(UserDataOut == null || UserDataOut.length == 0 || UserDataOut==undefined))
    		{		
    			var myObject = eval('(' + UserDataOut + ')');
    			StaffCode=myObject["txtStaffCode"];
    			StaffName=myObject["txtName"];	
    		}
    		var inner='歡迎進入 '+StaffCode+'/'+StaffName+'！今天是'+(date.getFullYear()-1911)+'年'+(date.getMonth()+1)+'月'+date.getDate()+'日'+
            '<a class="btn_login" id="logoff" href="#">登出</a>';
            $(".date_info").html(inner);
            document.cookie = "date_info="+inner;
            HeadSettingSuccessCallback();
        	//document.cookie = "header_container"+$("#header_container").html();
        	//alert($("#header_container").html().length);
            
            $("#logoff").bind("click",function(){
                var obj={code:"Login",actionID:"LogoutUser"};
                $.ajax({
                    type: "POST",
                    url: "action.php",
                    data: obj,
                    success: function(out){
                        //alert(out);
                        document.location.href="./index.php";
                    }
                });
                
            }).unbind('onmouseup').unbind('onmousedown');
    	});
        
    }
}


function cTableControl(){
    var editor;
    var DatePickerSelectCallback=function(){};
    this.DatePickerSelectCallbackSetting=function(callback){
        DatePickerSelectCallback=null;
        DatePickerSelectCallback=callback;
    }
    this.textReplacement=function(target,keyword){
    	var input=$("input[name="+target+"]");
    	input.attr("value",keyword).css("color","#999999");
    	
    	var originalvalue = input.val();
    	input.focus( function(){
    		if( $.trim(input.val()) == originalvalue ){ input.val('').css("color","#000"); }
    	});
    	input.blur( function(){
    		if( $.trim(input.val()) == '' ){ input.val(originalvalue).css("color","#999");}
    	});
    }
    this.createEditor=function (editId)
    {
        if ( editor )
            return;
/*
        //var html = document.getElementById( 'editorcontents' ).innerHTML;

        // Create a new editor inside the <div id="editor">, setting its value to html
        var config = {width:'95%'};
        if (CKEDITOR.instances[editId]) {
    		CKEDITOR.instances[editId].destroy();
    	}
        $(editId).children('textarea').ckeditor(function(){
    		CKFinder.SetupCKEditor( this,'./themes/lena/ckfinder/' );
    	});
        //editor = CKEDITOR.appendTo( editId, config );
        */
    }

    this.removeEditor=function()
    {
        if ( !editor )
            return;

        // Retrieve the editor contents. In an Ajax application, this data would be
        // sent to the server or used in any other way.
        //document.getElementById( 'editorcontents' ).innerHTML = editor.getData();
        //document.getElementById( 'contents' ).style.display = '';

        // Destroy the editor.
        editor.destroy();
        editor = null;
    }
    this.SetEditorData=function(Data){
    	if ( !editor )
            return;
    	editor.setData(Data);
    }
    this.sendBackEditorData=function(editId){
        if ( !editor )
            return;
        var xhtml=editor.getData();
        
        var input='<input name="'+ editId+'" value="'+xhtml +'">';
        //alert(editor.getData());
        this.removeEditor();
        $("#"+editId).html(input);
        
    }
    this.tableReset=function(tableName){
    	if(tableName==undefined)
    		tableName="searchTable";
    	$("#"+tableName+" :text").each(function(){
    		$(this).attr("value","");
    	});
    	$("#"+tableName+" select").each(function(){
    		$(this).children("option:first").attr("selected",true);
    	});
    	$("#gosrhAreaID").html("");
    }
    //作者:Aaron
    //註解者:shanhu
    //subTalbeName:區塊表單名(分頁)
    //日期:2011/09/01
    //描述:建立各種類型欄位
    this.BuildInsertsubTable=function(subTableName)
    {
    	$("#"+subTableName+" .tableShortText").each(function(){
            var innerString=' <input name="'+ $(this).attr("id") +'" type="text" class="def" style="width:55px;"  />';
            $(this).html(innerString);
        });
        $("#"+subTableName+" .tableText").each(function(){
            var innerString=' <input name="'+ $(this).attr("id") +'" type="text" class="def"  />';
            $(this).html(innerString);
        });
        $("#"+subTableName+" .tableList").each(function(){
            var innerString='<select name="'+ $(this).attr("id") +'"></select>';
            $(this).html(innerString);
        });
        $("#"+subTableName+" .tableRadio").each(function(){
      
        });
        $("#"+subTableName+" .tableTextButton").each(function(){
            var innerString=' <input name="'+ $(this).attr("id") +'" type="text"  class="def" style="width:50px;"/>'+
            '<input type="button" />';
            $(this).html(innerString);
        });
        $("#"+subTableName+" .tableUpload").each(function(){
            var innerString=' <input name="'+ $(this).attr("id") +'" type="file"  />';
            $(this).html(innerString);
        });
        $("#"+subTableName+" .tableTextarea").each(function(){
            var innerString='<textarea name="'+$(this).attr("id")+'"  rows="5" class="def" cols="150"></textarea>';
            $(this).html(innerString);
        });
        $("#"+subTableName+" .tableDate").each(function(){
            var innerString=' <input name="'+ $(this).attr("id") +'" type="text"  class="def" />';
            $(this).html(innerString);
            $('.tableDate input[name='+$(this).attr("id") +']').datepicker({changeMonth: true,
            	changeYear: true,
                onSelect:function(){
                	//alert("aa");
                DatePickerSelectCallback($(this).attr("name"));
                }
            });
           // $('input[name='+$(this).attr("id") +']').datepicker({changeMonth: true,changeYear: true});
            //alert($(this).attr("id"));
        });
        $("#"+subTableName+" .tableDiv").each(function(){
            var innerString='<div name="'+$(this).attr("id")+'" ></div>';
            $(this).html(innerString);
        });
        $("#"+subTableName+" .tablePopupSelect").each(function(){
            //if($(this).html().length==0){//mark這個條件是為了解決更新的時候無法輸入的問題 
            var innerString=' <input name="'+ $(this).attr("id") +'" type="text"  class="def" />'+
            '<input type="button" class="btn_select_s"/>';
            $(this).html(innerString);
            //}
            //alert($(this).attr("id"));
        });
    };
    this.BuildInsertTable=function(HtmlContainer)//ex: HtmlContainer='#xxx'
    {
        var HtmlContainer=HtmlContainer||'';
        if(HtmlContainer!=''){HtmlContainer=HtmlContainer+' ';}

        var createEdit=this.createEditor;
        var removeEdit=this.removeEditor;
        //alert("BuildInsertTable");
        $(HtmlContainer+".tableEditor").each(function(){
            
            removeEdit();
            var editor ="editor"+$(this).attr("id");
            //alert(editor);
            var innerString=' <textarea name="'+editor  +'" id="'+editor+'"  /></textarea>';
            $(this).html(innerString);
            createEdit(editor);
            //$("#"+editor).show();
        });
        $(HtmlContainer+".tableText").each(function(){
            //var suffix = $(this).attr('data-suffix')||'',
           // var attr=$(this).attr('data-inputattr')||'';
            var innerString=' <input name="'+ $(this).attr("id") +'" type="text"  class="def" ></input>';
            $(this).html(innerString);
        });     
        $(HtmlContainer+" .tableShortText").each(function(){
            var innerString=' <input name="'+ $(this).attr("id") +'" type="text" class="def" style="width:55px;"></input>';
            $(this).html(innerString);
        });

        $(HtmlContainer+".tableList").each(function(){
            var innerString='<select name="'+ $(this).attr("id") +'"></select>';
            $(this).innerHTML=innerString;
        });
/*L marked:
 
    $(HtmlContainer+".tableCheckboxs").each(function(){
      
    });*/
        $(HtmlContainer+".tableRadio").each(function(){

        });
        $(HtmlContainer+".tableTextButton").each(function(){
            var innerString=' <input name="'+ $(this).attr("id") +'" type="text" class="def" style="width:50px;" />'+
            '<input type="button" />';
            $(this).html(innerString);
        });
        $(HtmlContainer+".tableDivButton").each(function(){
            var innerString='<div name="'+$(this).attr("id")+'"  ></div>'+
            '<input type="button" />';
            $(this).html(innerString);
        });
        $(HtmlContainer+".tableUpload").each(function(){
            var innerString=' <input name="'+ $(this).attr("id") +'" type="file"  />';
            $(this).html(innerString);
        });
        $(HtmlContainer+".tableTextarea").each(function(){
            var innerString='<textarea name="'+$(this).attr("id")+'" class="def" rows="5" style="width:95%;"></textarea>';
            $(this).html(innerString);
        });
        $(HtmlContainer+".tableDate").not(":text").each(function(){
        	var suffix=$(this).attr('data-suffix')||'';
            var innerString=' <input name="'+ $(this).attr("id") +'" type="text"  class="def" />'+suffix;
            $(this).html(innerString);
            $('.tableDate input[name='+$(this).attr("id") +']').datepicker({changeMonth: true,
            	changeYear: true,
                onSelect:function(){
                	
                DatePickerSelectCallback($(this).attr("name"));
                }
            });
      //^^^^^^^^^^^ for more stable

            //alert($(this).attr("id"));
        });
        $(HtmlContainer+".tableDiv").not(".tableInsertHide").each(function(){
            var innerString='<div name="'+$(this).attr("id")+'" ></div>';
            $(this).html(innerString);
        });
        $(HtmlContainer+".tableInsertHide").hide();
        
        $(HtmlContainer+".tablePopupSelect").each(function(){
      if($(this).attr('data-protect_selectui')!=1){//this if is add by L in order to protect $(HtmlContainer+".tablePopupSelect :input").each() <select> UI
            //if($(this).html().length==0){//本來用沒有html代表她不是搜尋欄位
            //但是這有可能會有更新狀態的時候 已經將value加進去 所以她的html就不是空的
            //這會導致無法更新的狀況
            //alert($(this).attr("id")+" __ "+$(this).attr("name"));
            if($(this).attr("name")==undefined||$(this).attr("name").indexOf("gosrh")==-1)
            {
            var innerString=' <input onkeypress="return false" name="'+ $(this).attr("id") +'" type="text" class="def"  />'+
            '<input type="button" class="btn_select_s"/>';
            $(this).html(innerString);
            }
            //}
            //alert($(this).attr("id"));
      }
        });
        //$(HtmlContainer+".tableInsertHide").hide();

    //.jq is used at <td>(same as .table);.jq will 'effect' <input/>, not create <input/>(diff from .table)
    //$(HtmlContainer+".jqDate").each(function(){
    //  $(this).find('input').datepicker({changeMonth: true,changeYear: true});
    //});
};
    
this.insertDataToTableforUpload=function(myObj){
        //alert(index);
        //var returnvalue=myObj[index][returnID];
        //alert(returnvalue+"  "+index);
    $(".tableInsertHide").show();
    for( key in myObj )  
    {
        //alert(key);
        if(myObj!=undefined)
        {
            var $td=$('#'+key),
            className = $td.attr("class");
          if(className==undefined){
        	  //alert(key);
          }
          else if(className.indexOf("tableTextarea")!=-1)
          {$("#"+key+" textarea").attr("value",myObj[key]).html(myObj[key]);}
          else if(className.indexOf("tableText")!=-1)
          {
            $("#"+key+" :text").attr("value",myObj[key]);
          }
          else if(className.indexOf("tableShortText")!=-1)
          {
            $("#"+key+" input").attr("value",myObj[key]);
          }
          else if(className.indexOf("tableList")!=-1)
          { 
            //alert(key);
            var index2=myObj[key];
            $("#"+key+" select").children().each(function(){
                
              if ($(this).html()==index2){
                //jQuery給法
                $(this).attr("selected","true"); //或是給selected也可
              }
            }); 
          }
          else if(className.indexOf("tableCheckboxs")!=-1)
          {
            /*
               var div1='value="',
               str=$td.html(),
               s = str.substr( str.indexOf(div1)+div1.length ),
               div2='"',
               cur_chk_values = s.substr( 0, s.indexOf(div2) ),
               _id=$td.attr('id');


               var _html = '<input type="text" style="display:none" value="'+cur_chk_values+'" name="'+_id+'" />';
               var ary_chks = cMyGetList.returnList(_id);
               for(var k in ary_chks){
               var chk=false;
               for(var k //fail
               var str_chk=(chk)?' checked ':'';
               _html+='<label><input type="checkbox"'+str_chk+' value="'+k+'" />'+ary_chks[k]+'</label>';
               }*/
            $td.html(myObj[key]
                //+'updating'
                );

          }
          else if(className.indexOf("tableRadio")!=-1)
          {}
          else if(className.indexOf("tableUpload")!=-1)
          {}


          else if(className.indexOf("tableDate")!=-1)
          {$("#"+key+" input").attr("value",myObj[key]);}
          else if(className.indexOf("tableDiv")!=-1 && className.indexOf("tableDivButton")==-1)
          { 
           // $("#"+key+" div").html(myObj[key]);
        	  $("#"+key).html(myObj[key]);//他原本就沒有div

          }
          else if(className.indexOf("tablePopupSelect")!=-1)
          {
            $("#"+key).children(":first").attr("value",myObj[key]);
          }
          else if(className.indexOf("tableTextButton")!=-1)
          {
            //alert(myObj[key]);
            $("#"+key+" input").not(":button").attr("value",myObj[key]);
          }
          else if(className.indexOf("tableDivButton")!=-1){
        	  $("#"+key+" div").html(myObj[key]);
          }
          else{
        	  //alert(key);
          }
          
        }
        }

    $('.tableCheckboxs').find('input:checkbox').attr('disabled',false);
        //alert(myObj[index]+"  " +index+"  "+returnID+"  "+myObj[index][returnID]);
        //return returnvalue;
        
    };
    this.SaveReview=function(tableName){
        $("#"+tableName+" td").each(function(){
        	//aaron add 這邊會有錯誤要改
        	//alert($(this).attr("class"));
    		var className=$(this).attr("class");
    		if(className.length>0 && className!="borderleft" && className!="borderRight"){
    			if(className.indexOf("tableList")!=-1 ){
    				var val=$(this).children('select').find("option:selected").text();
    				var selectIndex=$(this).children('select').get(0).selectedIndex;
    				
				    if(selectIndex!=0||val=='是'||val=='否'){
				    	$(this).html(val);
				    }
				    else
				    	$(this).html('');
				   // $("#"+key).html(mytemparray[key]);
    	        }
    			else if(className.indexOf("tableDiv")!=-1 )
				{
    				
				}
    			else if(className.indexOf("tablePopupSelect")!=-1 ){
    				$(this).html($(this).children(" :text").val());
    			}
    			else if(className.indexOf("tableTextarea")!=-1 )
    			{   
    				var count=$(this).children('textarea').size();
    		    	if(count==1 && $(this).children().size()==1)
    		    		$(this).html($(this).children().val());
    		    }
    			else
    				$(this).html($(this).children().val());
    			
				
				
             }
        });
    }
    this.insertDataToSubTableforReviewbyOneArray=function(mytemparray,returnID,ListObj,tableName){

        $("#"+tableName+" td").each(function(){
            //aaron add 這邊會有錯誤要改
            //alert($(this).attr("class"));
        	var className=$(this).attr("class");
            if(className.length>0 && className!="borderleft" && className!="borderRight"){
                $(this).html("");
                //alert($(this).attr("id"));
            }
        });
        /*h06再累別按檢視 前一個子頁籤有東西會被刪掉
        $(".tableDate").add(".tablePopupSelect").each(function(){
            //alert($(this).children(":first").attr("id"));`
            //查詢欄位的tablePopupSelect 的child會有id不能清除查詢欄位
            //alert($(this).children(":first").attr("id"));
            var tempid=$(this).children(":first").attr("id");
            if(tempid==undefined||tempid.length==0||tempid.indexOf("gosrh")==-1)
                $(this).html("");
        });
        */
        $(".tableInsertHide").show();
        for( key in mytemparray )  
        {
            var className=$("#"+key).attr("class");
            
            if(className==undefined)
                continue;
            if(className.indexOf("tableText")!=-1 )
            {   $("#"+key).html(mytemparray[key]);}
            else if(className.indexOf("tableList")!=-1 )
            {
                if(key=="txtCityID" ||key=="txtAreaID"||key=="txtRoadID" ){
                    $("#"+key).html(mytemparray[key.replace("ID","")]);
                }
                else if(key=="txtContCity" ||key=="txtContArea"||key=="txtContRoad" ){
                    //$("#"+key).html(mytemparray[key.replace("ID","")]);
                    $("#"+key).html(mytemparray[key]);
                }
                else if(key=="txtCaseCity" ||key=="txtCaseArea"||key=="txtCaseRoad" ){
                    //$("#"+key).html(mytemparray[key.replace("ID","")]);
                    $("#"+key).html(mytemparray[key]);
                }
                else if(key=="txtContCityID" ||key=="txtContAreaID"||key=="txtContRoadID" ){
                    //$("#"+key).html(mytemparray[key.replace("ID","")]);
                    $("#"+key).html(mytemparray[key.replace("ID","")]);
                }
                else if(key=="txtCaseCityID" ||key=="txtCaseAreaID"||key=="txtCaseRoadID" ){
                    //$("#"+key).html(mytemparray[key.replace("ID","")]);
                	//alert(mytemparray[key.replace("ID","")]);
                    $("#"+key).html(mytemparray[key.replace("ID","")]);
                }
                else if(key=="OwnershiptxtHOwnerCity" ||key=="OwnershiptxtHOwnerArea"||key=="OwnershiptxtHOwnerRoad" ){
                    $("#"+key).html(mytemparray[key.replace("HOwner","")]); 
                }
                else if(key=="OwnershiptxtHPermanentCity" ||key=="OwnershiptxtHPermanentArea"||key=="OwnershiptxtHPermanentRoad" ){
                    $("#"+key).html(mytemparray[key.replace("txtH","txt")]); 
                }
                else if(key=="txtConsignorAgentCityID" || key=="txtConsignorAgentAreaID" || key=="txtConsignorAgentRoadID"){
					$('#'+key).html(mytemparray[key.replace("ID", "")]);
				}
                else if(key=="landtxtLocationCityID" || key=='landtxtLocationAreaID'){
					$('#'+key).html(mytemparray[key.replace('ID', '')]);
				}
                else if(key=="buildtxtCityID" || key=='buildtxtAreaID'){
					$('#'+key).html(mytemparray[key.replace('ID', '')]);
				}
                if(key=="txtHouseFuncID" ||key=="txtHouseFunc"){
                	
                    $(".tableHouseFunc").html(mytemparray["txtHouseFuncName"]);
                }
/*
 * var subtabkey=tempArray[i];
            //id=id.substring(id.indexOf("txt")+3,id.length);
            var key=subtabkey.substring(subtabkey.indexOf("txt"),subtabkey.length);//拿掉前致詞
            if(this.ListObj[key]!=undefined){
                var temp=this.ListObj[key];
                var innerString='<select name="'+subtabkey +'">';
                for(key2 in temp){
                    innerString+='<option value="'+key2+'">'+temp[key2] +'</option>';
                }
                innerString+="</select>";
                $("#"+subtabkey).html(innerString);//make UI!
            }
 */
                if(ListObj!=undefined){
                    var subtabkey=key;
                    key=key.substring(key.indexOf("txt"),key.length);//拿掉前致詞
                    //alert(subtabkey+"  "+key);
                    if(ListObj[key]!=undefined){
                        //alert(key);
                        var temp=ListObj[key];
                        var index=mytemparray[subtabkey];
                        //alert(mytemparray[key]);
                        //var tempp=temp[index];
                        if(temp[index]=="請選擇")
                            $("#"+subtabkey).html("");
                        else
                        $("#"+subtabkey).html(temp[index]);
                    }
                }
            }
            else if(className.indexOf("tableRadio")!=-1 )
            {}
            else if(className.indexOf("tableTextButton")!=-1 )
            {
                //alert(key);
                $("#"+key).html(mytemparray[key]);}
            else if(className.indexOf("tableUpload")!=-1 )
            {}
            else if(className.indexOf("tableTextarea")!=-1 )
            {
                $("#"+key).html(mytemparray[key]).attr('value',mytemparray[key]);
            }
            else if(className.indexOf("tableDate")!=-1 )
            {
                $("#"+key).html(mytemparray[key]);
            }
            else if(className.indexOf("tablePopupSelect")!=-1 )
            {
                //alert(key);
                $("#"+key).html(mytemparray[key]);
            }
            else if(className.indexOf("tableDiv")!=-1 )
            {
                
                $("#"+key).html(mytemparray[key]);
            }
            else 
            {
                $("#"+key).html(mytemparray[key]);
            }
        }
        return mytemparray[returnID];
    };
/**
*   //改善insertDataToTableforReview 一次要帶入所有obj進來
*   //這個是只要給一列資料就好
* @param 
* @param
* @param
* @author A
* @author L
* @return
*/
    this.insertDataToTableforReviewbyOneArray=function(mytemparray,returnID,ListObj, HtmlID){

        var HtmlID = HtmlID || "";
        $(HtmlID+".tableText").add(HtmlID+".tableList").add(HtmlID+".tableRadio").add(HtmlID+".tableTextButton").
        add(HtmlID+".tableUpload").add(HtmlID+".tableTextarea").add(HtmlID+".tableDiv")
        .add(HtmlID+".tableShortText").each(function(){
            //if($(this).attr("id").length==0)
            $(this).html("");
        });
    
        $(HtmlID+".tableDate").add(HtmlID+".tablePopupSelect").each(function(){
            //alert($(this).children(":first").attr("id"));`
            //查詢欄位的tablePopupSelect 的child會有id不能清除查詢欄位
            //alert($(this).children(":first").attr("id"));
            var tempid=$(this).children(":first").attr("id");
            if(tempid==undefined||tempid.length==0||tempid.indexOf("gosrh")==-1)
                $(this).html("");
        });

        $(".tableInsertHide").show();
        for(key in mytemparray)  
        {
            var $td=$("#"+key),
            className=$td.attr("class");
            if(className==undefined)
                continue;
            if(className.indexOf("tableText")!=-1 )
            {   
                $("#"+key).html(mytemparray[key]);
            }
            else if(className.indexOf("tableList")!=-1 )
            {
                if(key=="txtCityID" ||key=="txtAreaID"||key=="txtRoadID" ){
                    $("#"+key).html(mytemparray[key.replace("ID","")]);
                }

                /* for b02 (second CityID, AreaID and RoadID)*/
                if(key=="txtCityID2" ||key=="txtAreaID2"||key=="txtRoadID2" ){
                    $("#"+key).html(mytemparray[key.replace("ID2","Home")]);
                }

                if(key=="txtContCity" ||key=="txtContArea"||key=="txtContRoad" ){
                    //$("#"+key).html(mytemparray[key.replace("ID","")]);
                    $("#"+key).html(mytemparray[key]);
                }
                if(key=="txtCaseCity" ||key=="txtCaseArea"||key=="txtCaseRoad" ){
                    //$("#"+key).html(mytemparray[key.replace("ID","")]);
                    $("#"+key).html(mytemparray[key]);
                }
                if(key=="txtContCityID" ||key=="txtContAreaID"||key=="txtContRoadID" ){
                    //$("#"+key).html(mytemparray[key.replace("ID","")]);
                    $("#"+key).html(mytemparray[key.replace("ID","")]);
                }
                if(key=="txtCaseCityID" ||key=="txtCaseAreaID"||key=="txtCaseRoadID" ){
                    //$("#"+key).html(mytemparray[key.replace("ID","")]);
                    $("#"+key).html(mytemparray[key.replace("ID","Name")]);
                }
                //alert(key);
                if(key=="txtHouseFuncID"|| key=="txtHouseFunc"){ 
                	
                    $(".tableHouseFunc").html(mytemparray["txtHouseFuncName"]);

                }
                
                if(ListObj!=undefined){
                    //alert(key);
                    if(ListObj[key]!=undefined){
                        //alert(key);
                        var temp=ListObj[key];//L_NOTE: a row of a group-values(ex: from a same iListType)
                        //alert(temp);
                        var index=mytemparray[key];//L_NOTE: index means iListID
                        //alert(index+" "+temp[index]);
                        if(temp[index]=="請選擇")
                            $("#"+key).html("");
                        else
                        $("#"+key).html(temp[index]);
                    }
                }
            }
            else if(className.indexOf("tableCheckboxs")!=-1)
            {
		        var _id=$td.attr('id')||'';
		        if(_id){
		          var single_row=ListObj[_id],
		            str_chked_value=mytemparray[_id]||'',
		              ary_checked=(str_chked_value.indexOf(CHECKBOX_VALUE_SPLIT)!=-1)?str_chked_value.split(CHECKBOX_VALUE_SPLIT):[],//split ';741;742;' by ';'
		              _html='<input type="text" style="display:none" name="'+_id+'" value="'+str_chked_value+'" />';//i'm fail...cGetList.tableCheckboxs_initUI(_id);
		
		          for(var k in single_row){
		            var chk=false;
		            for(var k2 in ary_checked){
		              if(parseInt(ary_checked[k2])==k){chk=true;break;}
		            }
		            var str_chk=(chk)?' checked ':'';
		            _html+='<label><input type="checkbox"'+str_chk+' value="'+k+'" />'+single_row[k]+'</label>';//i'm fail...cGetList.tableCheckboxs_initCheckbox(k,single_row[k],chk);
		          }
		
		          $td.html(_html//+'updated'
		              );
		
		        }
            }
	      	else if(className.indexOf("tableRadio")!=-1 )
            {}
            else if(className.indexOf("tableTextButton")!=-1 )
            {
                
                $("#"+key).html(mytemparray[key]);
            }
            else if(className.indexOf("tableUpload")!=-1 )
            {}
            else if(className.indexOf("tableTextarea")!=-1 )
            {
               // $("#"+key).html(mytemparray[key]).attr('value',mytemparray[key]);
            	//$("#"+key).html(mytemparray[key]);
            	document.getElementByID(key).innerHTML=mytemparray[key];
            }
            else if(className.indexOf("tableDate")!=-1 )
            {
                $("#"+key).html(mytemparray[key]);
            }
            else if(className.indexOf("tablePopupSelect")!=-1 )
            {
                $("#"+key).html(mytemparray[key]);
            }
            else if(className.indexOf("tableDiv")!=-1 )
            {
                
                $("#"+key).html(mytemparray[key]);
            }
            else 
            {
                $("#"+key).html(mytemparray[key]);
            }
        }

        $('.tableCheckboxs').find('input:checkbox').attr('disabled',true);//<--when updated, disabled ui for all tableCheckboxs's checkboxs dis-clickable
        return mytemparray[returnID];
    };
    //以後會刪掉不用
    this.insertDataToTableforReview=function(myObj,index,returnID,ListObj){
        $(".tableText").add(".tableList").add(".tableRadio").add(".tableTextButton").
        add(".tableUpload").add(".tableTextarea").each(function(){
            //if($(this).attr("id").length==0)
            $(this).html("");
        });
        $(".tableDate").add(".tablePopupSelect").each(function(){
            //alert($(this).children(":first").attr("id"));`
            //查詢欄位的tablePopupSelect 的child會有id不能清除查詢欄位
            if($(this).children(":first").attr("id")==undefined||$(this).children(":first").attr("id").length==0)
            {
                if($(this).attr("name")==undefined || $(this).attr("name").indexOf("gosrh")==-1)
                    $(this).html("");
            }
        });
        $(".tableInsertHide").show();
        var mytemparray=myObj[index];
        for( key in mytemparray )  
        {
            
            var className = $("#"+key).attr("class");
            if(className == undefined || typeof className == undefined)
                continue;
            if(className.indexOf("tableText")!=-1)
            {
                $("#"+key).html(mytemparray[key]);
            }
            else if(className.indexOf("tableList")!=-1)
            {
                if(ListObj!=undefined){
                    
                    if(ListObj[key]!=undefined){
                        //alert(key);
                        var temp=ListObj[key];
                        var index=mytemparray[key];
                        $("#"+key).html(temp[index]);
                    }
                }
            }
            else if(className.indexOf("tableRadio")!=-1)
            {}
            else if(className.indexOf("tableTextButton")!=-1)
            {
                //alert(key);
                $("#"+key).html(mytemparray[key]);}
            else if(className.indexOf("tableUpload")!=-1)
            {}
            else if(className.indexOf("tableTextarea")!=-1)
            {
                $("#"+key).html(mytemparray[key]).attr('value',mytemparray[key]);
            }
            else if(className.indexOf("tableDate")!=-1)
            {
                $("#"+key).html(mytemparray[key]);
            }
            else if(className.indexOf("tablePopupSelect")!=-1)
            {
                //alert(key);
                $("#"+key).html(mytemparray[key]);
            }
            else if(className.indexOf("tableDiv")!=-1)
            {
                //alert(key);
                $("#"+key).html(mytemparray[key]);
            }
        }
        return mytemparray[returnID];
    };
    this.insertDataToTable=function(myObj,index,returnID){
        //alert("ID:"+myObj[returnID]);
        for( key in myObj[index] )  
        {
            //alert(key+"  "+myObj[index][key]);
            $("#"+key).attr("value",myObj[index][key]);
            //var needShowTag=searchShowItem[i].replace("nametxt","txt");
            //if(needShowTag==key){
                //item.push(Customers[key]);
                //break;
            //}
        }
        return myObj[index][returnID];
        
    };
    this.showPopup=function(innerString,width,height)
    {
    	
    	var tempWidth=420;
    	var tempHeight=100;
    	if(width!=undefined)
    		tempWidth=width;
    	if(height!=undefined)
    		tempHeight=height
       
    	if(width!=undefined){
    		$.Pop.open();
    		$.Pop.expand(innerString,tempWidth,tempHeight,function(){});
    	}
    	else{
    		alert(innerString);
    		$.Pop.close();
    	}
    };
    var togetherObj=new Array();
    for(var i=0;i<39/*30+(A+B*5+C)*2=37. before L add D01: i<30*/;i++)
        togetherObj[i]=new Array(2);

    togetherObj[0][0]="txtStaffCode";
    togetherObj[0][1]="txtStaffName";
    togetherObj[1][0]="txtBrancdCode";
    togetherObj[1][1]="txtBrancdName";
    togetherObj[2][0]="txtReceiverCode";
    togetherObj[2][1]="txtReceiverName";
    togetherObj[3][0]="txtHStaffCode";
    togetherObj[3][1]="txtHStaffName";
    togetherObj[4][0]="txtShopCode";
    togetherObj[4][1]="txtShopName";
    togetherObj[5][0]="txtCommCode";
    togetherObj[5][1]="txtCommName";
    togetherObj[6][0]="txtGuardianCode";
    togetherObj[6][1]="txtGuardianName";
    togetherObj[7][0]="txtCustCode";
    togetherObj[7][1]="txtCustName";
    togetherObj[8][0]="txtFormClassID";
    togetherObj[8][1]="txtFormClassName";
    togetherObj[9][0]="txtBranchCode";
    togetherObj[9][1]="txtBranchName";
    togetherObj[10][0]="gosrhBranchCode";
    togetherObj[10][1]="gosrhBranchName";
    togetherObj[11][0]="gosrhAreaCode";
    togetherObj[11][1]="gosrhAreaName";
    togetherObj[12][0]="gosrhCompanyType";
    togetherObj[12][1]="gosrhCompanyTypeName";
    togetherObj[13][0]="gosrhHeadCode";
    togetherObj[13][1]="gosrhHeadName";
    togetherObj[14][0]="gosrhBossCode";
    togetherObj[14][1]="gosrhBossName";
    togetherObj[15][0]="gosrhSecretaryCode";
    togetherObj[15][1]="gosrhSecretaryName";
    togetherObj[16][0]="gosrhBossCode";
    togetherObj[16][1]="gosrhBossName";
    togetherObj[17][0]="gosrhStaffCode";
    togetherObj[17][1]="gosrhName";
    togetherObj[18][0]="gosrhTakerCode";
    togetherObj[18][1]="gosrhTakerName";
    togetherObj[19][0]="gosrhFormClassID";
    togetherObj[19][1]="gosrhFormClassName";
    togetherObj[20][0]="txtTakerCode";
    togetherObj[20][1]="txtTakerName";
    togetherObj[21][0]="gosrhReceiverCode";
    togetherObj[21][1]="gosrhReceiverName";
    togetherObj[22][0]="gosrhStoreCode";
    togetherObj[22][1]="gosrhStoreName";
    togetherObj[23][0]="txtStoreCode";
    togetherObj[23][1]="txtStoreName";

    togetherObj[24][0]="txtGroupCode";
    togetherObj[24][1]="txtGroupName";
    togetherObj[25][0]="txtFormTakeStateID";
    togetherObj[25][1]="txtFormTakeStateName";
    togetherObj[26][0]="gosrhFormTakeStateID";
    togetherObj[26][1]="gosrhFormTakeStateName";
    togetherObj[27][0]="gosrhGuardianCode";
    togetherObj[27][1]="gosrhGuardianName";
    togetherObj[28][0]="gosrhOBranchCode";
    togetherObj[28][1]="gosrhOBranchName";
    togetherObj[29][0]="gosrhNBranchCode";
    togetherObj[29][1]="gosrhNBranchName";

  //L add 0412 for D01
    togetherObj[30] = ['txtCaseStaffCodeA','txtCaseStaffNameA'];
    togetherObj[31] = ['txtCaseStaffCodeB1','txtCaseStaffNameB1'];
    togetherObj[32] = ['txtCaseStaffCodeB2','txtCaseStaffNameB2'];
    togetherObj[33] = ['txtCaseStaffCodeB3','txtCaseStaffNameB3'];
    togetherObj[34] = ['txtCaseStaffCodeB4','txtCaseStaffNameB4'];
    togetherObj[35] = ['txtCaseStaffCodeB5','txtCaseStaffNameB5'];
    togetherObj[36] = ['txtCaseStaffIDC','txtCaseStaffNameC'];

    togetherObj[37] = ['txtStaffCode','txtStaffName']; //表單請使用 txtStaffCode 及txtStaffName 的配對

    togetherObj[38] = ['gosrhGroupCode','gosrhGroupName'];
    togetherObj[39] = ['talktxtStaffCode','talktxtStaffName'];
    togetherObj[40] = ['managetxtStaffCode','managetxtStaffName'];
    togetherObj[41] = ['ManagetxtHStaffCode','ManagetxtHStaffName'];
    /* buyer data for B01 */
    togetherObj[42] = ['txtStaffCode_1','txtStaffName_1'];
    togetherObj[43] = ['txtStaffCode_2','txtStaffName_2'];
    togetherObj[44] = ['txtStaffCode_3','txtStaffName_3'];
    togetherObj[45] = ['txtStaffCode_4','txtStaffName_4'];
    togetherObj[46] = ['txtStaffCode_5','txtStaffName_5'];
    togetherObj[47] = ['txtStaffCode_6','txtStaffName_6'];
    
  //L add 0429 for D01 searchUI START
  togetherObj[48] = ['gosrhShopCode','gosrhShopName'];
  togetherObj[49] = ['gosrhCommCode','gosrhCommName'];
  togetherObj[50] = ['gosrhCaseStaffIDB1','gosrhCaseStaffNameB1'];
  //L add 0429 for D01 searchUI END

    togetherObj[51] = ['txtCaseBranchCode', 'txtCaseBranchName'];
    togetherObj[52] = ['txtCaseStaffCodeC', 'txtCaseStaffNameC'];
    togetherObj[53] = ['txtCaseShopCode', 'txtCaseShopName'];
    togetherObj[54] = ['txtCaseCommCode', 'txtCaseCommName'];
    togetherObj[55] = ['txtMessageToStaff', 'txtMessageToStaffName'];
    togetherObj[56] = ['txtCompanyType', 'txtCompanyTypeName'];
    togetherObj[57] = ['txtCaseCode', 'txtCaseName'];
   
    togetherObj[58] = ['txtSellerCode', 'txtSellerName'];
    togetherObj[59] = ['ContractAdmintxtBranchCode1', 'ContractAdmintxtBranchName1'];
    togetherObj[60] = ['ContractAdmintxtBranchCode2', 'ContractAdmintxtBranchName2'];
    togetherObj[61] = ['ContractAdmintxtBranchCode3', 'ContractAdmintxtBranchName3'];
    togetherObj[62] = ['ContractAdmintxtBranchCode4', 'ContractAdmintxtBranchName4'];
    togetherObj[63] = ['ContractAdmintxtStaffCode1', 'ContractAdmintxtStaffName1'];
    togetherObj[64] = ['ContractAdmintxtStaffCode2', 'ContractAdmintxtStaffName2'];
    togetherObj[65] = ['ContractAdmintxtStaffCode3', 'ContractAdmintxtStaffName3'];
    togetherObj[66] = ['ContractAdmintxtStaffCode4', 'ContractAdmintxtStaffName4'];
    togetherObj[67] = ['ContractAdmintxtDeveloperCode1', 'ContractAdmintxtDeveloperName1'];
    togetherObj[68] = ['ContractAdmintxtDeveloperCode2', 'ContractAdmintxtDeveloperName2'];
    togetherObj[69] = ['ContractAdmintxtDeveloperCode3', 'ContractAdmintxtDeveloperName3'];
    togetherObj[70] = ['gosrhBranchEnd', 'gosrhBranchEnd'];
    togetherObj[71] = ['gosrhDep_nam', 'gosrhDep_nam'];
    togetherObj[72] = ['ContractAdmintxtBuyerCode', 'ContractAdmintxtBuyerName'];
    togetherObj[73] = ['txtBroCaseID', 'txtBroCaseName'];
    togetherObj[74] = ['gosrhManageCode', 'gosrhManageName'];
    togetherObj[75] = ['txtBuyerBranchID', 'txtBranchName'];  //阿賓新增這行
    togetherObj[76] = ['txtStoreID', 'txtStoreName'];
    togetherObj[77] = ['BrotxtBroCaseCode', 'BrotxtBroCaseName'];
    togetherObj[78] = ['txtStaffCode2', 'txtStaffName2'];
    togetherObj[79] = ['gosrhManageCode', 'gosrhManageName'];
    togetherObj[80] = ['gosrhBuyerCode', 'gosrhBuyerName'];
    togetherObj[81] = ['dealtxtDeveloperCode', 'dealtxtDeveloperName'];
    togetherObj[82] = ['dealtxtOwnerCode', 'dealtxtOwnerName'];
    togetherObj[83] = ['dealtxtBuyerCode', 'dealtxtBuyerName'];
    togetherObj[84] = ['gosrhOwnerCode', 'gosrhOwnerName'];
    togetherObj[85] = ['gosrhStaffCode', 'gosrhStaffName'];
    togetherObj[86] = ['gosrhAreaID', 'gosrhAreaName'];
    togetherObj[87] = ['BrotxtStaffCode_6', 'BrotxtStaffName_6'];
    togetherObj[88] = ['txtDevelopID', 'txtDevelopName'];
    togetherObj[89] = ['ManagetxtStaffCode','ManagetxtStaffName'];
    togetherObj[90] = ['gosrhStaffCode','gosrhStaffID'];
    togetherObj[91] = ['gosrhHStaffCode','gosrhHStaffName'];
    togetherObj[92] = ['gosrhEmp_no','gosrhEmp_nam'];
    togetherObj[93] = ['txtStaffCode1', 'txtStaffName1'];
    togetherObj[94] = ['SaleReporttxtStaffCode', 'SaleReporttxtStaffName'];
    togetherObj[95] = ['ContractAdmintxtSignStaffCode', 'ContractAdmintxtSignStaffName'];
    togetherObj[95] = ['ContractAdmintxtLastfixStaffCode', 'ContractAdmintxtLastfixStaffName'];
    
    


    var PopupCallback=function(out){};
    this.PopCallbackSetting=function(callback){
        PopupCallback=callback;
    }
    this.SetPopUpContent=function(listName,MyList)
    {
        var innerString='<select id="Popup'+listName +
        '" name="popupSelectContent" class="multiselect"  style="width:500px;height:200px;display:none;">';
        innerString+='<option></option>';
        for(key in MyList)
        {
            innerString+='<option style="width:400px;" value="'+ key +'" class="PopupSelect">('+key+')'+MyList[key] +'</option>';
        }
        innerString+="</select>";
        $.Pop.expand(innerString,460,200,function(){
        	$(".multiselect").multiselect('destroy');
        	$(".multiselect").multiselect({
			    searchable: true, droppable: 'both'
			   	, dividerLocation: 0.55, hideselected: true,
			   	doubleClickCallBack:function(){
			   		var temp=$('select[name=popupSelectContent] option:selected').text();
			   		var Value=$('select[name=popupSelectContent]').attr("value");
			   		
			   		temp=temp.replace("("+Value+")","");
			   		
			   		var Name=$('select[name=popupSelectContent]').attr("id").replace("Popup","");
                    //alert(Name+"  "+temp);
                    $("#"+Name).attr("value",temp);
                    //DevStaffName_0
                    Name=Name.replace("Name","Code");
                    //alert(Name);
                    $('#'+Name).attr("value",Value);
                    
                    $.Pop.close();
			   	}
			});   
        });
    }
    function SetPopUpContent(listName,MyList)
    {
    	
        //listName=listName.repl
        var returnID=listName;//.replace("gosrh","txt");//為了搜尋(gosrh)的群組權限功能
        var innerString='<select id="Popup'+listName +
        '" name="popupSelectContent" class="multiselect"  style="width:500px;height:200px;display:none;">';
        var myTogether=new Array();
        //alert(listName);
        //alert(togetherObj.length);
        //L_NOTE: the lastest way....foreach
        
        for(var i=0;i<togetherObj.length;i++)
        {
            if(togetherObj[i][0]==returnID||togetherObj[i][1]==returnID){
                myTogether[myTogether.length]=togetherObj[i];
            }   
        }


        innerString+='<option></option>';
        if(myTogether.length==0){//L_NOTE: the if() meaning: if not in myTogether, it use MyList, and MyList is cMyGetList.ListObj??
            
        	for(key in MyList)
            {
                innerString+='<option value="'+ key +'" class="PopupSelect" style="width:400px;">'+MyList[key] +'</option>';
            }
        }
        else
        {
            //alert(myTogether);
            for(key in MyList)
            {
                innerString+='<option style="width:400px;" value="'+ key +'" class="PopupSelect">('+key+')'+MyList[key] +'</option>';
            }
        }
        innerString+="</select>";
        $.Pop.expand(innerString,460,200,function(){
        	//$(".multiselect").children().remove();
        	//$(".multiselect").append(optionString);
        	
        	$(".multiselect").multiselect('destroy');
        	$(".multiselect").multiselect({
			    searchable: true, droppable: 'both'
			   	, dividerLocation: 0.55, hideselected: true,
			   //	maxcount: -1,
			   	doubleClickCallBack:function(){
			   		var temp=$('select[name=popupSelectContent] option:selected').text();
			   		
			   		var Name=$('select[name=popupSelectContent]').attr("id").replace("Popup","");
                    var Value=$('select[name=popupSelectContent]').attr("value");
                    if(myTogether.length!=0){
                        //alert($(this).html().replace("("+Value+")",""));
                        //alert(myTogether[0]+"  "+myTogether[1]);

                        //here is 自動帶入 START
                    	for(var i=0;i<myTogether.length;i++){
	                        $('#'+myTogether[i][0]).attr("value",Value);
	                        //alert($('#'+myTogether[1]).attr("class"));
	                        if($('#'+myTogether[i][1]).attr("class")==undefined)
	                        	$('#'+myTogether[i][1]).html(temp.replace("("+Value+")",""));
	                        else if($('#'+myTogether[i][1]).attr("class").indexOf("tableDiv")==-1)
	                        	$('#'+myTogether[i][1]).attr("value",temp.replace("("+Value+")",""));
	                        else
	                        	$('#'+myTogether[i][1]).html(temp.replace("("+Value+")",""));
                    	}
                        //here is 自動帶入 END
                    }
                    else{
                    	$('input[name='+Name+']').attr("value",temp);
                        $("#"+Name).attr("value",temp);

                        var input_id_value = "";
                        input_id_value = $('input[name=' + Name + 'ID]');
                        if(input_id_value != undefined)
                            input_id_value.attr("value",Value);
                    }
                    Name=$('select[name=popupSelectContent]').attr("id").replace("Popuptxt","txt");
                    //Value=$(this).attr("value");
                    //alert(Name);
                    if(myTogether != undefined){
                        //alert($(this).html().replace("("+Value+")",""));
                        //alert(myTogether[0]+"  "+myTogether[1]);
                    	for(var i=0;i<myTogether.length;i++){
	                        $('input[name='+myTogether[i][0]+']').attr("value",Value);
	                        $('input[name='+myTogether[i][1]+']').attr("value",temp.replace("("+Value+")","")); 
                    	}
                    }
                    else{
                        $('input[name='+Name+']').attr("value",temp);
                    }
                        
                        PopupCallback(Name);
                        $.Pop.close();
                    }
			   	}

				);
           
            
        });
    }
    this.PopupSelectSetting=function(cMyGetList,others)
    {
        //員工談出視窗放這
        var buttons;
        if(others!=undefined)
        	buttons=$(other);
        else
        	buttons=$(".tablePopupSelect :button");
        
        buttons.attr("value","選擇").unbind('click').bind('click',function(){//for popup格式的
        	
            $.Pop.open();
            
            //要replace("gosrh","txt") 才可以在搜尋的時候查到tlist的資料 因為名字開頭都txt
            //for search////
            var tempID=$(this).siblings("input").attr("id");
            var listName=tempID.replace("gosrh","txt");
            ////end search setting//////
            ////for content//////
            if(listName.length==0){//新增的時候
            	listName=$(this).siblings("input").attr("name");
            	tempID=listName;
            }
            ///end content setting/////
            
            var idArray=new Array();
            $(".tableSchool :text").each(function(){
                var id=$(this).attr("name");
                idArray[idArray.length]=id;
            });
            //idArray[idArray.length]=listName;
            
            cMyGetList.deleteListBase(idArray);
           // cMyGetList.deleteList(listName);
            //var MyList=cMyGetList.returnList(listName);
            var MyList={};
           // if(MyList==undefined)//list沒有這個欄位 要特別處理
            {
                
                cMyGetList.getOtherList(tempID,//listName, 改成tempID 為了 查詢
                    function(out){
                    //alert(out);
                    //alert(SetPopUpContent);
                   var getName= cMyGetList.getgetName();
                   var getID = cMyGetList.getgetID();
                   // MyList=cMyGetList.returnList(listName);
                	if(!(out == null || out.length == 0 || out==undefined))
                    {
                      //alert(out);
                        //alert(listName);
                    	
                        var myObject = eval('(' + out + ')');
                        //var tempArray = {};
                        
                        for(var i=0;i<myObject.length;i++){
                            //alert(myObject[i][getID]);
                            //alert(getID+"  "+getName);
                        	MyList[myObject[i][getID]]=myObject[i][getName]; 
                        }   
                    }
                    SetPopUpContent(tempID,MyList);
                    MyList=null;
                    delete MyList;
                    out=null;
                    delete out;
                    myObject=null;
                    delete myObject;
                    //$.Pop.close();
                    //return;
                    });
                
                //return false;
            }
            
        });
      
       
        cMyGetList.getSearchList();

        try{
        	$('.tableDate').datepicker({changeMonth: true,changeYear: true});
        }
        catch(err)
        {}
    }

}
function cGetList(){
    var ListObj=new Object();
    var returnIDObj=new Object();
    var getID="";//要回傳的id
    var getName="";//露出的名稱
    var getReutrn="";//已經有code + name的話 又要傳回id就只能用這個
    var schoolCity="";
    var schoolArea="";
    //tableList
    this.getgetName = function(){
    	return getName;
    }
    this.getgetID = function(){
    	return getID;
    }
    this.schoolOptionSet=function(City,Area){
        schoolCity=City;
        schoolArea=Area;
    }
    this.getCommonListAuto=function(callbackFunction)
    {
        
        //alert("auto");
        var arrayType=new Array();
        /*
        $("#"+tableName+" .tablePopupSelect").each(function (n)
        {
            //alert(n+"  "+$(this).attr("name").replace("gosrh","txt"));
            arrayType[arrayType.length]=$(this).attr("name").replace("gosrh","");
        });
        */
        $(".tableList").add(".tableCheckboxs")
        .each(function(n){
            var id=$(this).attr("id");
            if(id.indexOf("gosrh")!=-1)
            	id=id.substring(id.indexOf("gosrh")+5,id.length);
            else
            	id=id.substring(id.indexOf("txt")+3,id.length);
            arrayType[arrayType.length]=id;
        });

        if(arrayType.length>0)
        {
            var dataStr = {code: "List", actionID: "getCommonListArray", txtListTypeArray: arrayType};
            
            $.ajax({
                type: "POST",
                url: "list.php",
                data: dataStr,
                success: callbackFunction
            });
        }
        else
            callbackFunction("");
    };
    this.setOtherList= function(out,listName)
    {
        //alert(out);
        if(!(out == null || out.length == 0 || out==undefined))
        {
          //alert(out);
            //alert(listName);
        	
            var myObject = eval('(' + out + ')');
            var tempArray = {};
            
            for(var i=0;i<myObject.length;i++){
                //alert(myObject[i][getID]);
                //alert(getID+"  "+getName);
                tempArray[myObject[i][getID]]=myObject[i][getName]; 
            }
            this.insertListBase(tempArray,listName);
            
            tempArray=null;
            delete tempArray;
            
            
            out=null;
            delete out;
            myObject=null;
            delete myObject;
        }
    }
    this.getReturnID = function(CodeTag,Code,NameTag,Name){
        //alert(CodeTag+" _ " +Code+" _ "+NameTag+" _ "+Name);
        if(this.returnIDObj==undefined)
            return "";
        
        if(Code!=undefined && Code.length>=0 &&this.returnIDObj[CodeTag]!=undefined){
            //alert(CodeTag+"  "+Code);
            return this.returnIDObj[CodeTag][Code];
        }
        else if(Name!=undefined && Name.length>=0){
            //alert(NameTag+" "+Code);
            if(this.returnIDObj[NameTag]==undefined)
                return "";
            return this.returnIDObj[NameTag][Code];
        }
        return "";
    }
    this.getSearchList=function(){
    	$("#searchTable .tableList").each(function(){
    		//listName.replace("gosrh","txt")
    		//alert($(this).children().size());
    		if($(this).children().size()>1)
    		return;
    		var listName=$(this).attr("id");
    		var key=listName.replace("gosrh","txt");
    		//alert(listName);
    		
    		cMyGetList.getOtherList(listName,
	            function(out){
    			
	            //alert(out);
	            //alert(SetPopUpContent);
    			if(out==undefined){}
    			else
    			{
    				cMyGetList.setOtherList(out,key);
	           // MyList=cMyGetList.returnList(listName);
	          //  SetPopUpContent(listName,MyList);
    			}
	            	
	            	var temp=cMyGetList.returnList(key);
	            	//alert(temp);
	            	if(temp==undefined)
	            		return;
	            	var innerString='<option value=""></option>';
	            	for(key2 in temp){
	                    innerString+='<option value="'+key2+'">'+temp[key2] +'</option>';
	                }
	            	$("#"+listName).html(innerString);
	            	
	            
	            });
    		
    	});
    	
    }
    //這個是取得大表的list
    this.getOtherList = function(listName,callback)
    {
        //$.Pop.close();
        //alert(listName);
        var dataStr ;
        getReutrn="";
        switch(listName)
        {
        case "ManagetxtCommHouseCode":
        	getID="txtCommHouseID";
            getName="txtHLiverCode";
            dataStr= {code: "Community", actionID: "searchCommHouse"};
        	break;
        case "gosrhHouseFuncID":
            getID="txtHouseFuncID";
            getName="txtHouseFuncName";
            dataStr= {code: "List", actionID: "getHouseTypeusage"};
            break;
        case "gosrhFormTakeState":
        case "gosrhFormTakeStateID":
        case "txtFormTakeStateName":
            getID="txtFormTakeStateID";
            getName="txtFormTakeStateName";
            dataStr= {code: "Contract", actionID: "searchContract",table_name:"tContractSituation",
                    pk_name:"cContractSituationID"};
            break;
        case "gosrhCompanyType":
        case "gosrhCompanyTypeName":
            getID="txtCompanyType";
            getName="txtCompanyTypeName";
            dataStr= {code: "Branch", actionID: "searchBranchList"};
            break;
        case "BranchNameInput":
        case "gosrhBrancdCode":
        case "gosrhBranchCode":
        case "gosrhBranchName":
        case "gosrhStoreCode":
        case "gosrhStoreName":
        case "txtStoreCode":
        case "txtStoreName":
        case "txtCaseBranchCode":
        case "gosrhBranchEnd":
        case "gosrhDep_nam":
        case "txtStoreID":
            getID="txtBranchCode";
            getName="txtBranchName";
            dataStr= {code: "Branch", actionID: "searchBranchSimplified"};
            break;
        case "gosrhAreaCode":
        case "gosrhAreaID": 
        case "gosrhAreaName":
            getID="txtAreaCode";
            getName="txtAreaName";
            dataStr= {code: "Branch", actionID: "searchBranchList"};
            break;
        case "txtEstablishmentCode":
        case "txtBrancdCode":
        case "txtBrancdName":
        case "txtBranchCode":
        case "txtBranchName":
        case "gosrhOBranchCode":
        case "gosrhOBranchName":
        case "gosrhNBranchCode":
        case "gosrhNBranchName":
        case "ContractAdmintxtBranchCode1":
        case "ContractAdmintxtBranchCode2":
        case "ContractAdmintxtBranchCode3":
        case "ContractAdmintxtBranchCode4":
        case "gosrhContBranchName":
        case "txtBuyerBranchID":       //阿賓新增這行
            getReutrn="txtBranchID";//為了做抓id的動作
            getID="txtBranchCode";
            getName="txtBranchName";
            dataStr= {code: "Branch", actionID: "searchBranchSimplified"};
        break;
        case "dealtxtDeveloperCode":
        case "txtName":
        case "gosrhStaffName":
        case "gosrhStaffCode":
        case "gosrhStaffID":
        case "gosrhName":
        case "gosrhSecretaryCode":
        case "gosrhSecretaryName":
        case "gosrhBossCode":
        case "gosrhBossName":
        case "txtStaffName":
        case "txtStaffCode":
        case "txtStaffCode_1":
        case "txtStaffName_1":
        case "txtStaffCode_2":
        case "txtStaffName_2":
        case "txtStaffCode_3":
        case "txtStaffName_3":
        case "txtStaffCode_4":
        case "txtStaffName_4":
        case "txtStaffCode_5":
        case "txtStaffName_5":
        case "txtStaffCode_6":
        case "txtStaffName_6":                                         
        case "txtCaseStaffCodeA":
        case "txtCaseStaffNameA":
        case "txtCaseStaffCodeC":
        case "txtCaseStaffNameC":
        case "txtStaffID":
        case "txtReceiverName":
        case "txtReceiverCode":
        case "txtGuardianName":
        case "txtGuardianCode":
        case "txtCustName":
        case "txtCustCode":
        case "txtHStaffCode":
        case "txtHStaffName":
        case "gosrhTakerCode":
        case "gosrhTakerName":
        case "talktxtStaffName":
        case "talktxtStaffCode":
       
       
        
        case "gosrhReceiverCode":
        case "gosrhReceiverName":
        case "gosrhGuardianCode":
        case "gosrhGuardianName":
        case "ManagetxtHStaffName":
        case "ManagetxtHStaffCode":
        case "gosrhCaseStaffIDB1":
        case "gosrhCaseStaffNameB1":
        case 'txtCaseStaffIDA':
        case 'txtCaseStaffCodeB1':
        case 'txtCaseStaffNameB1':
        case 'txtCaseStaffCodeB2':
        case 'txtCaseStaffNameB2':
        case 'txtCaseStaffCodeB3':
        case 'txtCaseStaffNameB3':
        case 'txtCaseStaffCodeB4':
        case 'txtCaseStaffNameB4':
        case 'txtCaseStaffCodeB5':
        case 'txtCaseStaffNameB5':
        case 'txtCaseStaffIDC':
        case 'txtMessageToStaff':
        case 'txtMessageToStaffName':
        case "managetxtStaffCode":
        case 'managetxtStaffName':
        case "ManagetxtStaffCode":
        case 'ManagetxtStaffName':
        case 'ContractAdmintxtStaffCode1':
        case 'ContractAdmintxtStaffCode2':
        case 'ContractAdmintxtStaffCode3':
        case 'ContractAdmintxtStaffCode4':
        case 'ContractAdmintxtDeveloperCode1':
        case 'ContractAdmintxtDeveloperCode2':
        case 'ContractAdmintxtDeveloperCode3':
        case 'gosrhManageName':
        case 'txtStaffCode1':
        case 'txtStaffName1':
        case 'txtStaffCode2':
        case 'txtStaffName2':
        case 'gosrhManageName':
        case 'BrotxtStaffName_6':
        case 'txtDevelopID':
        case 'txtDevelopName':
        case 'gosrhHStaffCode':
        case 'gosrhHStaffName':
       	case 'gosrhEmp_no':
       	case 'gosrhEmp_nam':
       	case 'SaleReporttxtStaffCode':
       	case 'SaleReporttxtStaffName':
       	case 'ContractAdmintxtSignStaffCode':
       	case 'ContractAdmintxtLastfixStaffCode':
            getID="txtStaffCode";
            getName="txtName";
            dataStr= {code: "Login", actionID: "searchUserData_AccordingRights"};
            break;
    
        case "txtFormClassID":
        case "txtFormClassName":
        case "gosrhFormClassID":
        case "gosrhFormClassName":

        //alert("hi");
            getID="txtFormClassID";
            getName="txtFormClassName";
            dataStr= {code: "Contract", actionID: "searchContract"};
            dataStr["table_name"] = 'tContract';
            dataStr["pk_name"] = 'cContractID';
            dataStr["txtPathName"]=location.pathname;
            
            break;
        case "gosrhMETRO":
        case "txtMetro":
        case "txtMETRO":
            getID="txtStationID";
            getName="txtStationName";
            dataStr= {code: "List", actionID: "getAllMRTStation"};
            break;
        case "gosrhShopCode":
        case "gosrhShopName":
        case "txtShopCode":
        case "txtShopName":
        case "txtCaseShopCode":
        case "txtCaseShopName":
        case "txtNeedShopID":
            getID="txtShopCode";
            getName="txtShopName";
            var dataStr = {code: "Community", actionID: "searchShop"};
            break;
        case "gosrhCommCode":
        case "gosrhCommName":
        case "txtCommCode":
        case "txtCommName":
        case "txtCaseCommCode":
        case "txtCaseCommName":
        case "txtNeedCommID":
        
            getReutrn="txtCommID";
            getID="txtCommCode";
            getName="txtCommName";
            var dataStr = {code: "Community", actionID: "searchCommunity"};
            break;
        case "txtNearJuniorHigh":
        case "txtNearJuniorHighID":
        //case "txtCommName":
            getID="txtJunHiID";
            getName="txtLabel";
            //alert($(this).parent().attr("id"));
            //val =$(this).find('option:selected').attr("value")+"";
            var scity=$("#"+schoolCity).find('option:selected').html()+"";
            var sarea=$("#"+schoolArea).find('option:selected').html()+"";
            //alert(schoolCity+" "+schoolArea);
            //alert(scity+"  "+sarea);
            
            var dataStr = {code: "List", actionID: "getJuniorHigh", txtCity:scity, txtArea:sarea};
            break;
        case "txtNearElementary":
        case "txtNearElementaryID":
            //case "txtCommName":
                getID="txtElemID";
                getName="txtName";
                //alert($(this).parent().attr("id"));
                //val =$(this).find('option:selected').attr("value")+"";
                var scity=$("#"+schoolCity).find('option:selected').html()+"";
                var sarea=$("#"+schoolArea).find('option:selected').html()+"";
                //alert(schoolCity+" "+schoolArea);
                //alert(scity+"  "+sarea);
                
                var dataStr = {code: "List", actionID: "getElementary", txtCity:scity, txtArea:sarea};
                break;
        
        case "gosrhHeadCode"://這個是為了search用 所以id我設定跟name一樣 這樣子他回傳id去後台的時候
        case "gosrhHeadName":
            //才會回傳headcode 而不是id
            getID="txtHeadCode";
            getName="txtHeadName";
            var dataStr = {code: "Branch", actionID: "searchHead"};
            break;
        case "gosrhGroupCode":
        case "gosrhGroupName":
        case "txtGroupCode":
        case "txtGroupName":
            getID="txtGroupCode";
            getName="txtGroupName";
            var dataStr = {code: "ACL", actionID: "searchGroup"};
            break;
        case "txtFormTakeStateID":
        case "txtFormTakeStateName":
        
            getID="txtFormTakeStateID";
            getName="txtFormTakeStateName";
            var dataStr = {code: "Contract", actionID: "searchConstractSituset"};
            //tContractSituation 
            break;
        case "txtCompanyType":
        	getID="txtManageCode";
            getName="txtCity";
            var dataStr = {code: "List", actionID: "getCity"};
        	break;
        case "gosrhOwnerName":
        case "txtOwnerName":
        case "dealtxtOwnerCode":
        	
        	getID="txtCaseCode";
            getName="txtOwnerName";
        	var dataStr = {code: "Seller", actionID: "searchSeller"};
        	break;
        case "txtSellerCode":
        case "dealtxtBuyerCode":
        	getID="txtCaseCode";
            getName="txtCaseName";
        	var dataStr = {code: "Seller", actionID: "searchSeller"};
        	break;
        case "ContractAdmintxtBuyerCode":
        case "gosrhBuyerCode":
        case "gosrhBuyerName":
        	getID="txtBuyerCode";
            getName="txtBuyerName";
        	var dataStr = {code: "Buyer", actionID: "searchBuyer"};
        	break;
        case "txtTemplateSmessage":
        	getID="txtTemplateID";
            getName="txtTemplateContent";
        	var dataStr = {code: "Msg", actionID: "searchMsgTemplate"};
        	break;
        case "txtBroCaseID":
        case "BrotxtBroCaseCode":
        case "BrotxtBroCaseName":
        	getID="txtCaseCode";
            getName="txtCaseName";
        	var dataStr = {code: "Case", actionID: "searchCase"};
        	break;
        	
        }
        if(dataStr==undefined){
        	callback();
        	return;
        }
        $.ajax({
            type: "POST",
            url: "action.php",
            data: dataStr,
            async:false,
            success: callback
        });
        //alert(listName);
    }
    this.getCommonList=function(arrType,callbackFunction)
    {
        //var arrType = new Array(3);
        //arrType[0] = 'gender';
        //arrType[1] = 'armystate';
        //arrType[2] = 'bloodtype';
        var dataStr = {code: "List", actionID: "getCommonListArray", txtListTypeArray: arrType};
        $.ajax({
            type: "POST",
            url: "list.php",
            data: dataStr,
            
            success: callbackFunction
        });
    };

    this.tableCheckboxs_innerHTML=function($td){
    $td.find('input:checkbox').live('click',function(){
            var $chkboxs_container = $(this).parent().parent(),
                $input_hidden = $chkboxs_container.find('input[name]'),
                v='';
            $chkboxs_container.find('input:checkbox:checked').each(function(){
              v+=($(this).val()+CHECKBOX_VALUE_SPLIT);
            });

            $input_hidden.val(CHECKBOX_VALUE_SPLIT+v);
          }); 
    };

    this.tableCheckboxs_initUI=function(input_name){
        return '<input type="text" style="display:none" name="'+input_name+'" value="" />';//we get off above line, use this line, because base.js's this line...if(this.type=="select-one" || this.type=="text" ||this.type=="textarea")

    };
    this.tableCheckboxs_initCheckbox=function(value,display,is_checked){
        var is_checked=is_checked||'',
        checked=(is_checked)?' checked ':'';
        return '<label><input type="checkbox"'+checked+' value="'+value+'" />'+display+'</label>';
    };
    this.setCommonListForSubTab=function(tableName){
        //alert(tableName);
        var tempArray=new Array();
        $("#"+tableName+" .tableList").each(function (n){
            
            //alert($(this).attr("id"));
            var key=$(this).attr("id");
            tempArray[tempArray.length]=key;
        });
        for(var i=0;i<tempArray.length;i++){
            var subtabkey=tempArray[i];
            //id=id.substring(id.indexOf("txt")+3,id.length);
            var key=subtabkey.substring(subtabkey.indexOf("txt"),subtabkey.length);//拿掉前致詞
            if(this.ListObj[key]!=undefined){
                var temp=this.ListObj[key];
                var innerString='<select name="'+subtabkey +'">';
                innerString+='<option>請選擇</option>';
                for(key2 in temp){
                    innerString+='<option value="'+key2+'">'+temp[key2] +'</option>';
                }
                innerString+="</select>";
                $("#"+subtabkey).html(innerString);//make UI!
            }
        }
        this.setOherCommonList(tableName);
    }
    //這邊可以加上base list和其他特殊的list
	//作者:Aaron
    //註解者:shanhu
    //HtmlContainer:
    //日期:2011/09/01
    //描述:建立基本的List和特殊的List
    this.setCommonList=function(HtmlContainer){
        var HtmlContainer = HtmlContainer||'';
        if(HtmlContainer != ''){ HtmlContainer = HtmlContainer + ' ';}
        var _cssclass;
        var _id;
        me = this;
        for(key in this.ListObj)
        {
            var $td = $(HtmlContainer + "#"+key);
            _cssclass = $td.attr("class");
            _id = $td.attr("id");
            if(_cssclass == 'undefined' || _id == 'undefined')
                continue;
            
            var temp=this.ListObj[key];

            //if(_cssclass!="tablePopupSelect" && _cssclass!="tableText")
            
            if(_cssclass!=undefined && _cssclass.indexOf("tablePopupSelect")==-1 && _cssclass.indexOf("tableText")==-1)
            {
                //switch(_cssclass){
                //  case'tableList':
                //  default:
                if(_cssclass.indexOf('tableList')!=-1
                    || _cssclass.indexOf('table')==-1//<---this means like <td id="txtFieldName" class="borderRight"></td> & those <td> of No CssClass, NOT means <td id="txtFieldName" class="tableXXXX borderRight"></td>
                    ){
                            var innerString='<select name="'+key +'">';
                            innerString+='<option>請選擇</option>';
                            for(key2 in temp){
                                innerString+='<option value="'+key2+'">'+temp[key2] +'</option>';
                            }
                            innerString+="</select>";
                   }
                   //   break;
                   //   case 'tableCheckboxs':
            
                        //var innerString='<input type="hidden" name="'+key+'" value="" />';
               if(_cssclass.indexOf('tableCheckboxs')!=-1){
                        var innerString=this.tableCheckboxs_initUI(key);
                        for(var k in temp){
                          innerString+=this.tableCheckboxs_initCheckbox(k,temp[k]);//'<label><input type="checkbox" value="'+k+'" />'+temp[k]+'</label>';
                        }
               }
              //    break;
              //  }
                
                $td.html(innerString);//make UI!
        
                //make UI event START
                if(_cssclass.indexOf('tableCheckboxs')!=-1){
                  this.tableCheckboxs_innerHTML($td);
                }
                //make UI event END

            }
        }
        this.setOherCommonList("");
        
        
    };
    var AreaSet=function(City,AreaKey,RoadKey,Area){
    	var cMyBase = new Base();
        var obj={};
        obj["code"] = "List";
        obj["actionID"] = "getArea";
        obj["txtCity"]=City;
        cMyBase.DataBaseConnectUsingObj(obj, function(out){
            var myObject = eval('(' + out + ')')||{};
            if(!myObject){return;}
            var innerString='<select name="'+AreaKey +'"><option>區域</option>';
            //alert(out);
            for(var i=0;i<myObject.length;i++)
            {
                var AreaCode=myObject[i]["txtAreaID"];
                var AreaName=myObject[i]["txtArea"];
                innerString+='<option value="'+AreaCode+'">'+AreaName +'</option>';  
            }
            innerString+="</select>";
            $('#'+AreaKey).html(innerString);
            $("#"+AreaKey+" select").children().each(function(){
                //alert($(this).html()+"  "+CityID);
                if ($(this).html()==Area){
                    $(this).attr("selected","true"); //或是給selected也可
                }
            });
            $('select[name='+AreaKey+']').change(function(){
            	var cMyBase = new Base();
                $(".tableSchool :text").attr("value","");
                var item2 = $('select[name='+AreaKey+']'+' option:selected').text();
                //alert("  "+item2);
                RoadSet(City,item2,RoadKey);
                var zipCodeObj={code:"List",actionID:"getZipCode",txtArea:item2,txtCity:City};
                cMyBase.DataBaseConnectUsingObj(zipCodeObj, function(out){
                	var myObject = eval('(' + out + ')');
                	//alert(myObject[0]["txtZip"]);
                	var myZip;
                	if(AreaKey.indexOf("ID")==-1)
                		myZip=AreaKey.replace("Area","Zip");
                	else
                		myZip=AreaKey.replace("AreaID","Zip");
                	$("#"+myZip).html(myObject[0]["txtZip"]);
                	//$('input[name='+myZip+']').attr('value',myObject[0]["txtZip"]);
                });
              //測試段小段 可能還需要修正細節
                LandListSetting(City,item2);
                
            });
            myObject=null;
            delete myObject;
        });
        cMyBase=null;
        delete cMyBase;
    }
    var RoadSet = function(City,Area,RoadKey,Road){
    	var cMyBase = new Base();
        var obj={};
    	obj["code"] = "List";
        obj["actionID"] = "getSection";
        obj["txtCity"]=City;
        obj["txtArea"]=Area;
        cMyBase.DataBaseConnectUsingObj(obj, function(out){
            var myObject = eval('(' + out + ')');
            //alert(out);
            var innerString='<select name="'+RoadKey +'"><option>路段以上</option>';
            for(var i=0;i<myObject.length;i++)
            {
                var RoadID=myObject[i]["txtRoadID"];
                var RoadName=myObject[i]["txtRoad"];
                innerString+='<option value="'+RoadID+'">'+RoadName +'</option>';  
            }
            innerString+="</select>";
            //alert(innerString);
            $('#'+RoadKey).html(innerString);
            $("#"+RoadKey+" select").children().each(function(){
                //alert($(this).html()+"  "+Road);
                if ($(this).html()==Road){
                    $(this).attr("selected","true"); //或是給selected也可
                }
            }); 
        });
    }
    this.AddressSet=function(City,Area,Road,CityKey,AreaKey,RoadKey){
        //alert(key);
        if(CityKey==undefined)
            CityKey="txtCityID";
        if(AreaKey==undefined)
            AreaKey="txtAreaID";
        if(RoadKey==undefined)
            RoadKey="txtRoadID";
        AreaSet(City,AreaKey,RoadKey,Area);
        RoadSet(City,Area,RoadKey,Road);
        LandListSetting(City,Area);
        
         
        $("#"+CityKey+" select").children().each(function(){
            //alert($(this).html()+"  "+CityID);
            if ($(this).html()==City){
                $(this).attr("selected","true"); //或是給selected也可
            }
        }); 
    }
    //tableName=""表示整頁的city都要被處理
    //只要其中一個Table被處理的話要填入tableName
    this.setOherCommonList=function(tableName){
        if(this.ListObj==undefined)
            return false;
        //類型 用途
        //var tempHouseType=this.ListObj["txtHouseTypeID"];
        
        
        //地址的縣市 鄉鎮 路段 屬於特殊案例 必須要特別處理
        var tempCity=this.ListObj["txtCityID"];
        //alert(tempCity);
        var tempCityKeyArray=new Array();
        if(tableName.length==0){
            $(".tableCity").each(function(n){
                tempCityKeyArray[tempCityKeyArray.length]=$(this).attr("id");
            });
        }
        else
        {
            $("#"+tableName+" .tableCity").each(function(n){
                tempCityKeyArray[tempCityKeyArray.length]=$(this).attr("id");
            })
        }
        for(var i=0;i<tempCityKeyArray.length;i++){
            var CityKey=tempCityKeyArray[i];
            var innerString='<select name="'+CityKey +'"><option>縣市</option>';
            for(key in tempCity){
                innerString+='<option value="'+key+'">'+tempCity[key] +'</option>';
            }
            innerString+="</select>";
            $('#'+CityKey).html(innerString);
            $('#'+CityKey.replace("City","Area")).html('<select><option>區域</option></select>');
            $('#'+CityKey.replace("City","Road")).html('<select><option>路段以上</option></select>');
            $('select[name='+CityKey+']').change(function(){//txtCityID change event
                $(".tableSchool :text").attr("value",""); 
                var key2=$(this).attr("name");
                var City = $('select[name='+key2+']'+' option:selected').text();
                var RoadKey=key2.replace("City","Road");
                var AreaKey=key2.replace("City","Area");
                $('.tableLand').html('<select><option>路段以上</option></select>');
                
                AreaSet(City,AreaKey,RoadKey);
            });
        };
        
        $(".tableHouseType").each(function(n){
            //alert("aa");
            var key=$(this).attr("id");
            $('select[name='+key+']').change(function(){
                var item = $('select[name='+key+']'+' option:selected').attr("value");
                var cMyBase = new Base();
                var obj={};
                obj["code"] = "List";
                obj["actionID"] = "getHouseTypeusage";
                obj["txtHouseTypeID"]=item;
                cMyBase.DataBaseConnectUsingObj(obj, function(out2){
                    //alert(out2);
                    //txtHouseTypeusageID":"45","txtHouseTypeusageNmae"
                    var myObject2 = eval('(' + out2 + ')');
                    var key2=$(".tableHouseFunc").attr("id");
                    //alert(key2);
                    var innerString='<select name="'+key2 +'"><option>請選擇</option>';
                    for(var i=0;i<myObject2.length;i++)
                    {
                        var Code=myObject2[i]["txtHouseFuncID"];
                        var temp=myObject2[i]["txtHouseFuncName"];
                        innerString+='<option value="'+Code+'">'+temp +'</option>'; 
                    }
                    innerString+="</select>";
                    $(".tableHouseFunc").html(innerString);
                });
                //alert(item);
                //txtHouseTypeID
            });
        });
    };
    var LandListSetting=function(City,Area){
    	if($(".tableLand").size()>0){
            //alert($(".tableLand").size());
    		var obj={};
            obj["code"] = "LandReport";
            obj["actionID"] = "getSection";
            obj["txtCity"]=City;
            obj["txtArea"]=Area;
            //alert(item+" "+item2);
            cMyBase.DataBaseConnectUsingObj(obj, function(outLand){
                var LandObject = eval('(' + outLand + ')');
                //alert(LandObject);
                //SESSION_NAME
                outLand=null;
                delete outLand;
                $(".tableLand").each(function(){
                	var name=$(this).attr("id");
                	var value=$(this).html();
                	
                	var innerLandString='<select name="'+name+'"><option>請選擇</option>';
                    for(var i=0;i<LandObject.length;i++){
                    	var key=LandObject[i]["SESSION_NAME"];
                        var temp=LandObject[i]["SESSION_NAME"];
                        innerLandString+='<option value="'+key+'">'+temp +'</option>';  
                    }
                    innerLandString+='</select>';
                	$(this).html(innerLandString);
                	$("#"+name+" select").children().each(function(){
                        //alert($(this).html()+"  "+CityID);
                        if ($(this).html()==value){
                            $(this).attr("selected","true"); //或是給selected也可
                        }
                    }); 
                	
                });
                LandObject=null;
                delete LandObject;
            });
                
            
            
        }
    };
    /*
     * HouseTypeID = 房屋形式的ID
     * HouseFunc = 房屋類別的名字
     */
    this.HouseTypeSet=function(HouseTypeID,HouseFunc){
//alert(HouseTypeID);
        var key=$(".tableHouseType").attr("id");
        $('select[name='+key+']').change(function(){
            var item = $('select[name='+key+']'+' option:selected').attr("value");
            var cMyBase = new Base();
            var obj={};
            obj["code"] = "List";
            obj["actionID"] = "getHouseTypeusage";
            obj["txtHouseTypeID"]=item;
            cMyBase.DataBaseConnectUsingObj(obj, function(out2){
                //alert(out2);
                //txtHouseTypeusageID":"45","txtHouseTypeusageNmae"
                var myObject2 = eval('(' + out2 + ')');
                var key2=$(".tableHouseFunc").attr("id");
                //alert(key2);
                var innerString='<select name="'+key2 +'"><option>請選擇</option>';
                for(var i=0;i<myObject2.length;i++)
                {
                    var Code=myObject2[i]["txtHouseFuncID"];
                    var temp=myObject2[i]["txtHouseFuncName"];
                    innerString+='<option value="'+Code+'">'+temp +'</option>'; 
                }
                innerString+="</select>";
                $(".tableHouseFunc").html(innerString);
            });
            //alert(item);
            //txtHouseTypeID
        });
        var cMyBase = new Base();
        var obj={};
        obj["code"] = "List";
        obj["actionID"] = "getHouseTypeusage";
        obj["txtHouseTypeID"]=HouseTypeID;
        cMyBase.DataBaseConnectUsingObj(obj, function(out2){
            //alert(out2);
            //txtHouseTypeusageID":"45","txtHouseTypeusageNmae"
            var myObject2 = eval('(' + out2 + ')');
            var key2=$(".tableHouseFunc").attr("id");
            //alert(key2);
            var innerString='<select name="'+key2 +'"><option>請選擇</option>';
            for(var i=0;i<myObject2.length;i++)
            {
                var Code=myObject2[i]["txtHouseFuncID"];
                var temp=myObject2[i]["txtHouseFuncName"];
                innerString+='<option value="'+Code+'">'+temp +'</option>'; 
            }
            innerString+="</select>";
            $(".tableHouseFunc").html(innerString);
            $(".tableHouseFunc select").children().each(function(){
                //alert($(this).html()+"  "+CityID);
                if ($(this).html()==HouseFunc){
                    $(this).attr("selected","true"); //或是給selected也可
                }
            }); 
            
        });
       
    }
    
  /*
   * a getter for ListObj
   *@return Object
   * */
    this.returnListObj=function(){
        return this.ListObj;
    }
    this.deleteList=function(listName){
    	if(this.ListObj==undefined)
            return ;
    	else if(this.ListObj[listName]==undefined)
    		return;
        else
        	this.ListObj[listName]=null;
    }
  /*
   *a getter for ListObj
   *@param String, listName, the key of ListObj
   *@return Object
   * */
    this.returnList=function(listName){
        if(this.ListObj==undefined)
            return ;
        else
        return this.ListObj[listName];
    }
    this.returnKey=function(listName,value){
        if(value==undefined)
            return "";
        if(value.length==0)
            return "";
        
        if(value<0)
            return "";
        else if(this.ListObj[listName]==undefined)
            return "";
        var tempList=this.ListObj[listName];
        for(key in tempList){
            if(tempList[key]==value)
                return key;
        }
        return "";
        
        /*
        else if(this.ListObj[listName][id]==null || this.ListObj[listName][id].length==0||
                this.ListObj[listName][id]==undefined){
            return "";}
        else if(this.ListObj[listName][id]=="請選擇")
            return "";
        else
            return this.ListObj[listName][id];
            */
    }
    //將id轉為value
    this.returnListValue=function(listName,id){
        //alert(listName+" "+id);
        if(id==undefined)
            return "";
        if(id.length==0)
            return "";
        
        if(id<0)
            return "";
        else if(this.ListObj[listName]==undefined)
            return "";
        else if(this.ListObj[listName][id]==null || this.ListObj[listName][id].length==0||
                this.ListObj[listName][id]==undefined){
            return "";}
        else if(this.ListObj[listName][id]=="請選擇")
            return "";
        else
            return this.ListObj[listName][id];
    }
    this.ListObjSet=function(out){
        //alert(out);
        var localListObj={};
        if(out.length==0)
        {
            this.ListObj= localListObj;
            return;
        }
        var myObject = eval('(' + out + ')');
        //alert(this.tempaaa.length);
        for(var j=0;j<myObject.length;j++)
        {
            var temp=myObject[j]["txtListValue"];//抓回傳的一筆tlist結構
            if(temp.length==0)
            	continue;
            var listType="txt"+temp[0]["iListType"];
            var tempArray={};
            if(temp[0]["iListName"]!="請選擇")
            	tempArray[temp[0]["iListID"]]=temp[0]["iListName"];
            for(var i=1;i<temp.length;i++){
                tempArray[temp[i]["iListID"]]=temp[i]["iListName"];
            }
            //alert(listType);
           // if(listType.length>0)
            localListObj[listType]=tempArray;
            
            this.insertListBase(tempArray,listType);
            
            
        }
       // this.ListObj= localListObj;
        
    };
    /*
    function(out) {
    alert(out);
    if(!(out == null || out.length == 0 || out==undefined))
    {
        var myObject = eval('(' + out + ')');
        var tempArray={};
        for(var i=0;i<myObject.length;i++){
            tempArray[myObject[i][id]]=myObject[i][name];
        }
        alert("hhh");
        insertListObj(id,tempArray);
        //alert(tempArray.length);
        callback(tempArray);
    }
}
*/
    this.insertreturnIDObjBase=function(tempArray,id){
        //alert(id);
        if(this.returnIDObj==undefined)
        {
            //alert("hiiii");
            var localListObj={};
            localListObj[id]=tempArray;
            this.returnIDObj= localListObj;
        }
        else
        {   
            //alert(id);
            this.returnIDObj[id]=tempArray;
        }
    }
/*
 *add this.ListObj key&value
 *@param tempArray, the value
 *@param id, the key
 */
    this.deleteListBase=function(id){
        //alert(id.length);
        for(var i=0;i<id.length;i++){
            if(this.ListObj[id[i]]!=undefined)
                delete this.ListObj[id[i]];
        }
    }
    this.insertListBase=function(tempArray,id){
        //alert(id);
        if(this.ListObj==undefined)
        {
            //alert("hiiii");
            var localListObj={};
            localListObj[id]=tempArray;
            this.ListObj= localListObj;
        }
        else
        {   
            //alert(id);
        	if(this.ListObj[id]!=null){
        		this.ListObj[id]=null;
        		delete this.ListObj[id];
        		
        	}
            this.ListObj[id]=tempArray;
        }
        delete tempArray;
    }
    this.insertYesNoList=function(ids){
        //alert(ids.length);
            var tempArray ={};
            tempArray["1"]="是";
            tempArray["0"]="否";
        for(var i=0;i<ids.length;i++){
            //alert(ids[i]);
            this.insertListBase(tempArray,ids[i]);
        }
    };
/*
 * @deprecated
 *
 * let yesORno UI behind have <input type="text" (the UI first use at D01 > tCase > EnvironmentCondition)
 * @param JSObject, ex: {'id1':1,'id2':1},  or {'id1':{'attr1':'attrval1','attr2':'attrval2'},'id2':{...}}
 * @author L
 */
  this.insertYesNoMoreInput=function(yesORnoUI__objjsdata){
    var cur_id='',
        cur_name='',
        $td;
    for(var k in yesORnoUI__objjsdata){
      cur_id=k;
      cur_name=cur_id.replace('txtHas','txtMore','g');
      $td = $('#'+cur_id);
      $td.html( $td.html() + '<input type="text" class="def" name="'+cur_name+'" />(多個請以;隔開)' );//<--cannot work
    }
  };
/*
 *@params out ArrayDataFromPHP
 *@params id the idname
 *@params name the namename
 *@return null
 */
    this.insertListObj=function(out,id,name)
    {
        //alert(out);
        if(!(out == null || out.length == 0 || out==undefined))
        {
            var myObject = eval('(' + out + ')');
            var tempArray={};
            for(var i=0;i<myObject.length;i++){
                tempArray[myObject[i][id]]=myObject[i][name];
            }
            this.insertListBase(tempArray,id);
        }
    };
    this.insertListObjWithDBOption=function(out,dataid,dataname,id,name)
    {
        //alert(out);
        if(!(out == null || out.length == 0 || out==undefined))
        {
            var myObject = eval('(' + out + ')');
            var tempArray={};
            for(var i=0;i<myObject.length;i++){
                tempArray[myObject[i][dataid]]=myObject[i][dataname];
            }
            this.insertListBase(tempArray,id);
        }
    };
     
    
    
    //this.getBranch = 
}

////////////////////////////////////////////
////public function/////////////////////////////











/*
function clearText(){
    $("input").each(function (n){
        if(this.type=="text")
        {
            //alert(this.type+"  "+this.value);
            this.value="";
        }
    });
}
*/
//////////////////////////////////////////
/*
function getGroupList()
{
    var dataStr = {code: "Group", actionID: "listGroups"};
    
    $.ajax({
        type: "POST",
        url: "list.php",
        data: dataStr,
        success: function(out) {
            //(result[item].src == null || result[item].src.length < 1)
        if(!(out == null || out.length == 0 || out==undefined))
        {
            var myObject = eval('(' + out + ')');
                        alert(out);
            }
        }
    });
}
function DataBaseConnectSearchCounter(code,actionID,tableName,Callback,startIdx,endIdx){
    var obj=getTextValue(code,actionID,tableName);
    obj["txtStartIdx"]=startIdx;
    obj["txtEndIdx"]=endIdx;
    $.ajax({
        type: "POST",
        url: "action.php",
        data: obj,
        success: Callback
    });
}
*/
