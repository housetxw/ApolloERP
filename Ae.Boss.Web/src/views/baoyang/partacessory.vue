<template>
  <main class="container">
<div class="topBar" style="margin-top:28px;">
<el-tabs type="border-card" v-model="activeName">
  <el-tab-pane label="选择车型" name="1">
  <div class="carSelect">
    <span class="input-label"> 品牌：</span>
    <el-select v-model="brand" @change="getVehicleByBrand();" filterable placeholder="-请选择品牌-" size="small">
        <el-option
        v-for="item in brandList"
        :key="item.brand"
        :label="item.brand"
        :value="item.brand">
        </el-option>
    </el-select>
    <span class="input-label">车系：</span>
    <el-select v-model="vehicleId" @change="getPaiLiangByVehicleId()" filterable placeholder="-请选择车系-" size="small">
        <el-option
        v-for="item in vehicleList"
        :key="item.vehicleId"
        :label="item.vehicle"
        :value="item.vehicleId">
        </el-option>
    </el-select>
    <span class="input-label"> 排量：</span>
    <el-select v-model="paiLiang" @change="getVehicleNianByPaiLiang()" filterable placeholder="-请选择排量-" size="small">
        <el-option
        v-for="item in paiLiangList"
        :key="item"
        :label="item"
        :value="item">
        </el-option>
    </el-select><span style="color:red;font-size:12px;margin-left:3px;">(*必选)</span>
    <br/> <br/>
    <span class="input-label"> 生产年份：</span>
    <el-select v-model="nian" @clear="tid='';salesNameList=[];" @change="getVehicleSalesNameByNian()" clearable filterable placeholder="-请选择生产年份-" size="small">
        <el-option
        v-for="item in nianList"
        :key="item"
        :label="item"
        :value="item">
        </el-option>
    </el-select>
    <span class="input-label"> 车款：</span>
    <el-select v-model="tid" clearable filterable placeholder="-请选择车款-" size="small">
        <el-option
        v-for="item in salesNameList"
        :key="item.tid"
        :label="item.salesName"
        :value="item.tid">
        </el-option>
    </el-select>
  </div>
  </el-tab-pane>
    <el-tab-pane label="输入VehicleID和排量" name="2">
        <div class="carSelect">
            <span class="input-label"> 输入VehicleID：</span>
            <el-input class="input" clearable size="small" placeholder="请输入VID" @change="getPaiLiangByVehicleIdV2()" v-model="secondVehicleId"></el-input>
            <span class="input-label"> 排量：</span>
            <el-select v-model="secondPaiLiang" filterable placeholder="-请选择排量-" size="small">
                <el-option
                    v-for="item in secondPaiLiangList"
                    :key="item"
                    :label="item"
                    :value="item">
                    </el-option>
            </el-select>
            <span style="color:red;font-size:12px;margin-left:3px;">(*必选)</span>
        </div>
    </el-tab-pane>
    <el-tab-pane label="输入TID" name="3">
        <span class="input-label"> 输入TID：</span>
        <el-input class="input" clearable size="small" placeholder="TID" v-model="secondTid"></el-input>
    </el-tab-pane>
</el-tabs>
</div>
<div class="buttons">
<el-row>
  <el-button type="success" size="small" @click="getBaoYangPartAccessory();">查询</el-button>
  <!--<el-button type="primary" size="small" @click="dialogFormVisible = true">批量添加</el-button>-->
</el-row>
</div>
<div class="box">
    <el-select v-model="accessoryName" multiple filterable placeholder="选择需要显示的配件" @change="partAccessoryChange()" style="margin-top:10px;width:500px;" size="small">
        <el-option
        v-for="item in accessoryConfig"
        :key="item.partName"
        :label="item.partName"
        :value="item.partName"></el-option>
    </el-select>
    <el-checkbox style="float:right;font-weight:bold;margin-right:5px;margin-top:17px;" v-model="checked" @change="showEdit()">修改数据</el-checkbox>
<el-dialog :width="dialogWidth" title="批量修改" :close-on-click-modal='false' :visible.sync="dialogFormVisible">
  <table class="dialog-addAttribute">
    <tr>
      <td style="text-align:center;color: #d43f3a; font-weight: bolder">
        <label>{{this.currentPartAccessory}}</label>
      </td>
    </tr>
    <tr>
      <td>
        <span class="input-title"> TID：</span>
        <el-select style="width:calc(100% - 60px);margin-left:5px;" v-model="addTid" multiple filterable placeholder="-选择添加的Tid-" size="small">
          <el-option
          v-for="item in baoYangPartAccessoryList"
          :key="item.tid"
          :label="item.tid"
          :value="item.tid">
          </el-option>
        </el-select>
      </td>
    </tr>
  </table>
  <table class="dialog-addAttribute" style="margin-top:-1px;" v-html="this.innerHtmlAttribute">
  </table>
  <span slot="footer" class="dialog-footer">
    <el-button type="primary" @click="batchEditAccessory();">提 交</el-button>
    <el-button @click="dialogFormVisible = false">取 消</el-button>
  </span>
</el-dialog>
</div>
<div>
  <div style="margin-top: 18px; margin-bottom: 18px" class="table-responsive">
    <table class="tableContainer" v-html="this.innerHtmlData"></table>
  </div>
</div>
</main>
</template>

<script>
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";
import { appSvc } from "@/api/baoyang/baoyang";
export default {
    name:'partacessory',
        data() {
      return {
        dialogWidth:'500px',
        innerHtmlData:'',
        innerHtmlAttribute:'',
        dialogFormVisible:false,
        brandList:[],
        vehicleList:[],
        paiLiangList:[],
        nianList:[],
        salesNameList:[],
        brand:'',
        vehicleId:'',
        paiLiang:'',
        nian:'',
        tid:'',
        secondVehicleId:'',
        secondPaiLiang:'',
        secondPaiLiangList:[],
        secondTid:'',
        activeName: '1',
        checked: false,
        accessoryConfig:[],
        accessoryName:[],
        baoYangPartAccessoryList:[],
        tableLoading:false,
        addTid:[],
        currentPartAccessory:''
      }
    },
    created(){
        this.getBrandList();
        this.getPartAccessoryConfig();
        window.hideEidtInput = this.hideEidtInput;
        window.submitEditAccessoryInfo = this.submitEditAccessoryInfo;
        window.showEditBYInput = this.showEditBYInput;
        window.deleteData = this.deleteData;
        window.showSelectEdit = this.showSelectEdit;
        window.saveSelectEditInfo = this.saveSelectEditInfo;
        window.editAccessories = this.editAccessories;
    },
    methods: {
        getBrandList(){
            appSvc.getBrandList().then(
            res => {
                var data = res.data;
                this.brandList = data;
            },
            error => {
                console.log(error)
            }
            )
            .catch(() => {})
            .finally(() => {});
        },
        getVehicleByBrand(){
            if(this.brand==''){
                return;
            }
            this.vehicleId='';
            this.paiLiang='';
            this.nian='';
            this.tid='';
            var vehicleCondition = {brand:this.brand};
            appSvc.getListByBrand(vehicleCondition).then(res=>{
                var data = res.data;
                this.vehicleList = data;
            },error=>{
                console.log(error)
            })
            .catch(()=>{})
            .finally(()=>{})
        },
        getPaiLiangByVehicleId(){
            if(this.vehicleId==''){
                return;
            }
            this.paiLiang='';
            this.nian='';
            this.tid='';
            var paiLiangRequest={vehicleId:this.vehicleId };
            console.log( JSON.stringify(paiLiangRequest));
            appSvc.getPaiLiangByVehicleId(paiLiangRequest).then(res=>{
                var data = res.data;
                this.paiLiangList = data;
            },error=>{
                console.log(error)
            })
            .catch(()=>{})
            .finally(()=>{})
        },
        getVehicleNianByPaiLiang(){
            if(this.vehicleId==''||this.paiLiang==''){
                return;
            }
            this.nian='';
            this.tid='';
            var nianRequest={vehicleId:this.vehicleId ,paiLiang :this.paiLiang}
            appSvc.getVehicleNianByPaiLiang(nianRequest).then(res=>{
                var data = res.data;
                this.nianList = data;
            },error=>{
                console.log(error)
            })
            .catch(()=>{})
            .finally(()=>{})
        },
        getVehicleSalesNameByNian(){
            if(this.vehicleId==''||this.paiLiang==''||this.nian==''){
                return;
            }
            this.tid='';
            var tidRequest={vehicleId:this.vehicleId ,paiLiang :this.paiLiang,nian :this.nian}
            appSvc.getVehicleSalesNameByNian(tidRequest).then(res=>{
                var data = res.data;
                this.salesNameList = data;
            },error=>{
                console.log(error)
            })
            .catch(()=>{})
            .finally(()=>{})
        },
        getPaiLiangByVehicleIdV2(){
            this.secondPaiLiang='';
            this.secondPaiLiangList=[];
            if(this.secondVehicleId==''){
                return;
            }
            var paiLiangRequest={vehicleId:this.secondVehicleId };
            appSvc.getPaiLiangByVehicleId(paiLiangRequest).then(res=>{
                var data = res.data;
                this.secondPaiLiangList = data;
            },error=>{
                console.log(error)
            })
            .catch(()=>{})
            .finally(()=>{})
        },
        getPartAccessoryConfig(){
            appSvc.getPartAccessoryConfig().then(
            res => {
                var data = res.data;
                this.accessoryConfig = data;
            },
            error => {
                console.log(error)
            }
            )
            .catch(() => {})
            .finally(() => {});
        },
        getBaoYangPartAccessory(){
          var tabType = this.activeName;
          var accessoryRequest={
              vehicleId:'',
              paiLiang:'',
              nian:'',
              tid:''};
          if(tabType=="1"){
              if(this.brand==''){
                  this.$message({message: "请选择品牌",type: "error"});
                  return;
              }
              if(this.vehicleId==''){
                  this.$message({message: "请选择车系",type: "error"});
                  return;
              }
              if(this.paiLiang==''){
                  this.$message({message: "请选择排量",type: "error"});
                  return;
              }
              accessoryRequest.vehicleId=this.vehicleId;
              accessoryRequest.paiLiang=this.paiLiang;
              accessoryRequest.nian=this.nian;
              accessoryRequest.tid=this.tid;
          }
          else if(tabType=="2"){
              if(this.secondVehicleId==''){
                  this.$message({message: "请输入车系Id",type: "error"});
                  return;
              }
              if(this.secondPaiLiangList==''){
                  this.$message({message: "请选择排量",type: "error"});
                  return;
              }
              accessoryRequest.vehicleId=this.secondVehicleId;
              accessoryRequest.paiLiang=this.secondPaiLiang;
          }
          else if(tabType=="3"){
              if(this.secondTid==''){
                  this.$message({message: "请输入Tid",type: "error"});
                  return;
              }
              accessoryRequest.tid=this.secondTid;
          }
          else{
              return;
          }
          this.tableLoading = true;
          console.log("condition: " + JSON.stringify(accessoryRequest));
          appSvc.getBaoYangPartAccessory(accessoryRequest).then(res=>{
              var data = res.data;
              this.baoYangPartAccessoryList = data;
              this.bindData();
          },error=>{
              console.log(error)
          })
          .catch(()=>{})
          .finally(()=>{
              this.tableLoading = false;
          })
        },
        bindData(){
          var data=this.baoYangPartAccessoryList;
          var partNames=this.accessoryConfig;
          var html="";
          var rowTid = "<tr><th>TID</th>";
          var rowVehicleID = "<tr><td style='font-weight:bolder'>VehicleID</td>";
          var rowVehicleName = "<tr><td style='font-weight:bolder'>车型名称</td>";
          var rowFdjName = "<tr><td style='font-weight:bolder'>发动机号</td>";
          var isExtus = false;
          for(var index = 0; index < data.length; index++){
            rowTid += "<th>" + data[index].tid + "</th>";
            var vehilceName = data[index].vehicleInfo.brandFromVehicleType + " | " + data[index].vehicleInfo.vehicleFromVehicleType + " | " +data[index].vehicleInfo.paiLiang + " | " + data[index].vehicleInfo.listedYear + "-" + data[index].vehicleInfo.stopProductionYear + "年产 | " + data[index].vehicleInfo.nian + "款" +data[index].vehicleInfo.salesName+" | "+data[index].vehicleInfo.transmissionTypeE;
            rowVehicleName += "<td>" + vehilceName + "</td>";
            rowVehicleID += "<td>" + data[index].vehicleInfo.vehicleId + "</td>";
            rowFdjName +="<td>" + data[index].vehicleInfo.engineNo + "</td>";
          }
          rowTid += "</tr>";
          rowVehicleID += "</tr>";
          rowVehicleName += "</tr>";
          rowFdjName += "</tr>";
          html += rowTid + rowVehicleID + rowVehicleName + rowFdjName;
          for(var index = 0; index < partNames.length; index++){
            var content = "<tr id='data_" + partNames[index].partName + "' style='"+this.showPartAccessory(partNames[index].partName)+"'><td style='font-weight:bolder'>" + partNames[index].partName + "</td>";
             for (var i = 0; i < data.length; i++) {
                var tid = data[i].tid;
                var details = data[i].details;
                isExtus = false;
                for (var j = 0; j < details.length; j++) {
                    if (details[j].accessoryName === partNames[index].partName) {
                        isExtus = true;
                        break;
                    }
                }
                if (isExtus) {
                    content += this.convertToHtml(tid,details, partNames[index].partName);
                } else {
                    content += this.noConvertToHtml(tid,partNames[index].partName);
                }
            }
            content += "</tr>";
            html += content;
          }
          this.innerHtmlData = html;
        },
        convertToHtml(tid, details, partName){
          var content = "";
          var isExtus = false;
          var detailIndex = 0;
          for (var i = 0; i < details.length; i++) {
              if (details[i].accessoryName === partName) {
                  isExtus = true;
                  detailIndex = i;
                  break;
              }
          }
          var operationEditDeleted = "<li class='ui-state-error' style='list-style-type:none;cursor:pointer;float:right' onclick='deleteData(this)'><span class='ui-icon ui-icon-trash'></span></li>";
          var operationAccessoryEdit = "<li  onclick='editAccessories(this)'><span style='cursor:pointer'>批量修改</span></li>";
          if(isExtus){
            for (var index = 0; index < this.accessoryConfig.length; index++){
              if (this.accessoryConfig[index].partName == partName){
                content += "<td id='" + tid + partName + "' class='clearPadding'><table id='tableAccessory' class='innerTable'>";
                for (var j = 0; j < this.accessoryConfig[index].display.length; j++){
                  var key = this.accessoryConfig[index].display[j].key;
                    switch (key) {
                        case "Volume":
                            if (this.IsNull(details[detailIndex].volume)) {
                                details[detailIndex].volume = "无数据";                                
                            }       
                            content += "<tr><td><span key='"+key+"'>" + this.accessoryConfig[index].display[j].value + "</span>：<a class='value-group'>" + details[detailIndex].volume + "</a>";
                            content += this.convertEdit(details[detailIndex].volume, key, tid, partName);
                            if (!this.IsNull(details[detailIndex].volume)) {
                                content += "&nbsp;<span  id='Unit" + tid + partName + "'>" + this.accessoryConfig[index].volume.unit + "</span>";
                            } 
                            break;
                        case "Level":
                            if (this.IsNull(details[detailIndex].level)) {
                                details[detailIndex].level = "无数据";
                            }
                            content += "<tr><td><span key='"+key+"'>" + this.accessoryConfig[index].display[j].value + "</span>：<a class='value-group'>" + details[detailIndex].level + "</a>";
                            content += this.convertEdit(details[detailIndex].level,key, tid, partName);
                            break;
                        case "Quantity":
                            if (details[detailIndex].quantity==0) {
                                details[detailIndex].quantity = "无数据";                                
                            } 
                            content += "<tr><td><span key='"+key+"'>" + this.accessoryConfig[index].display[j].value + "</span>：<a class='value-group'>" + details[detailIndex].quantity + "</a>";
                            content += this.convertEdit(details[detailIndex].quantity, key, tid, partName);
                            if (details[detailIndex].quantity>0) {
                                content += "&nbsp;<span  id='Unit" + tid + partName + "'>" + this.accessoryConfig[index].quantity.unit + "</span>";
                            } 
                            break;
                        case "Size":
                            if (this.IsNull(details[detailIndex].size)) {
                                details[detailIndex].size = "无数据";
                            }
                            content += "<tr><td><span key='"+key+"'>" + this.accessoryConfig[index].display[j].value + "</span>：<a class='value-group'>" + details[detailIndex].size + "</a>";
                            content += this.convertEdit(details[detailIndex].size,key, tid, partName);
                            break;
                        case "Interface":
                            if (this.IsNull(details[detailIndex].interface)) {
                                details[detailIndex].interface = "无数据";
                            }
                            content += "<tr><td><span key='"+key+"'>" + this.accessoryConfig[index].display[j].value + "</span>：<a class='value-group'>" + details[detailIndex].interface + "</a>";
                            content += this.convertEdit(details[detailIndex].interface,key, tid, partName);
                            break;
                        case "Description":
                            if (this.IsNull(details[detailIndex].description)) {
                                details[detailIndex].description = "无数据";
                            }
                            content += "<tr><td><span key='"+key+"'>" + this.accessoryConfig[index].display[j].value + "</span>：<a class='value-group'>" + details[detailIndex].description + "</a>";
                            content += this.convertEdit(details[detailIndex].description,key, tid, partName);
                            break;
                        case "Viscosity":
                            if (this.IsNull(details[detailIndex].viscosity)) {
                                details[detailIndex].viscosity = "无数据";
                            }
                            content += "<tr><td><span key='"+key+"'>" + this.accessoryConfig[index].display[j].value + "</span>：<a class='value-group'>" + details[detailIndex].viscosity + "</a>";
                            content += this.convertEdit(details[detailIndex].viscosity,key, tid, partName);
                            break;
                        case "Grade":
                          if(this.IsNull(details[detailIndex].grade)){
                            details[detailIndex].grade = "无数据";
                          }
                          content += "<tr><td><span key='"+key+"'>" + this.accessoryConfig[index].display[j].value + "</span>：<a class='value-group'>" + details[detailIndex].grade + "</a>";
                          content += this.convertEdit(details[detailIndex].grade,key, tid, partName);
                          break;
                        default:
                            break;
                    }
                    content += "</td></tr>";
                }
                content += "<tr><td><a tid="+tid+" accessory="+partName+" style='float:right;"+this.showOperation()+"' class='operation-group'>" + operationEditDeleted + "</a></td></tr>";
                content += "<tr><td><label style='display:none'>" + partName + "</label><label style='display:none'>" + tid + "</label><a class='BatchAdd-group' style='text-align:center;list-style:none;text-decoration:none;"+this.showOperation()+"'>" + operationAccessoryEdit + "</a></td></tr>";
                content += "</table></td>";
              }
            }
          }
          return content;
        },
        noConvertToHtml(tid, partName){
          var operationEditDeleted = "<li class='ui-state-error' style='list-style-type:none;cursor:pointer;float:right' onclick='deleteData(this)'><span class='ui-icon ui-icon-trash'></span></li>";
          var operationAccessoryEdit = "<li  onclick='editAccessories(this)'><span style='cursor:pointer'>批量修改</span></li>";
          var content = ""
          for (var index = 0; index < this.accessoryConfig.length; index++)
          {
            if (this.accessoryConfig[index].partName == partName) {
              content += "<td id='" + tid + partName + "' class='clearPadding'><table id='tableAccessory' class='innerTable'>";
              for (var j = 0; j < this.accessoryConfig[index].display.length; j++)
              {
                var key = this.accessoryConfig[index].display[j].key;
                  switch (key) {
                    case "Volume":
                        content += "<tr><td><span key='"+key+"'>" + this.accessoryConfig[index].display[j].value + "</span>：<a class='value-group'>" + "无数据" + "</a>";
                        content += this.noConvertEdit(key, tid, partName);
                        content += "&nbsp;<span style='display:none;' id='Unit" + tid + partName + "'>" + this.accessoryConfig[index].volume.unit + "</span>";
                        break;
                    case "Level":
                        content += "<tr><td><span key='"+key+"'>" + this.accessoryConfig[index].display[j].value + "</span>：<a class='value-group'>" + "无数据" + "</a>";
                        content += this.noConvertEdit(key, tid, partName);
                        break;
                    case "Quantity":
                        content += "<tr><td><span key='"+key+"'>" + this.accessoryConfig[index].display[j].value + "</span>：<a class='value-group'>" + "无数据" + "</a>";
                        content += this.noConvertEdit(key, tid, partName);
                        content += "&nbsp;<span style='display:none;' id='Unit" + tid + partName + "'>" + this.accessoryConfig[index].quantity.unit + "</span>";
                        break;
                    case "Size":
                        content += "<tr><td><span key='"+key+"'>" + this.accessoryConfig[index].display[j].value + "</span>：<a class='value-group'>" + "无数据" + "</a>";
                        content += this.noConvertEdit(key, tid, partName);
                        break;
                    case "Interface":
                        content += "<tr><td><span key='"+key+"'>" + this.accessoryConfig[index].display[j].value + "</span>：<a class='value-group'>" + "无数据" + "</a>";
                        content += this.noConvertEdit(key, tid, partName);
                        break;
                    case "Description":
                        content += "<tr><td><span key='"+key+"'>" + this.accessoryConfig[index].display[j].value + "</span>：<a class='value-group'>" + "无数据" + "</a>";
                        content += this.noConvertEdit(key, tid, partName);
                        break;
                    case "Viscosity":
                        content += "<tr><td><span key='"+key+"'>" + this.accessoryConfig[index].display[j].value + "</span>：<a class='value-group'>" + "无数据" + "</a>";
                        content += this.noConvertEdit(key, tid, partName);
                        break;
                    case "Grade":
                      content += "<tr><td><span key='"+key+"'>" + this.accessoryConfig[index].display[j].value + "</span>：<a class='value-group'>" + "无数据" + "</a>";
                      content += this.noConvertEdit(key, tid, partName);
                      break;
                    default:
                        break;
                  }
                  content += "</td></tr>";
              }              
              content += "<tr><td><a tid="+tid+" accessory="+partName+" style='float:right;"+this.showOperation()+"' class='operation-group'>" + operationEditDeleted + "</a></td></tr>";
              content += "<tr><td><label style='display:none'>" + partName + "</label><label style='display:none'>" + tid + "</label><a class='BatchAdd-group' style='text-align:center;list-style:none;text-decoration:none;"+this.showOperation()+"'>" + operationAccessoryEdit + "</a></td></tr>";
              content += "</table></td>";
          }
          }
          return content;
        },
        convertEdit(value,key, tid, partName) {
          var operationEditAddInput = "<li style='list-style-type:none;float:right' onclick='showEditBYInput(this)'><span class='ui-icon ui-icon-plus'></span></li>";
          var operationInputEdit = "<li style='list-style-type:none' onclick='showEditBYInput(this)'><span class='ui-icon ui-icon-pencil'></span></li>";
          var accessoryEditInput = "<input style='display:inline;width:50px;cursor:auto' type='text' onkeydown='submitEditAccessoryInfo(this)' onblur = 'hideEidtInput(this)'/>";
          var operationEditAddSelect = "<li onclick='showSelectEdit(this)' style='list-style-type:none;cursor:pointer;float:right'><span class='ui-icon ui-icon-plus'></span></li>";
          var operationSelectEdit = "<li  style='list-style-type:none' onclick='showSelectEdit(this)'><span class='ui-icon ui-icon-pencil'></span></li>";
          var accessoryEditSelect = "<select  class='selectEdit' style='display:none'><option values=''><option></select>";
          var operationBYSave = "<img style='cursor:pointer' src='"+require("../../assets/bao_yang_images/save.png")+"' onclick='saveSelectEditInfo(this)'style='display:inline'/>";
          var content = "";
          switch (key)
          {
              case "Volume":
              case "Quantity":
              case "Size":
                  if (value == "" || value == "无数据") {
                      content += "<ul class='operation-group' style='float:right;"+this.showOperation()+"'>" + operationEditAddInput + "</ul><ul class='operation-Add' style='display:none;float:right'>" + operationInputEdit + "</ul><a style=\"display:none\">" + tid + "</a><a style=\"display:none\">" + partName + "</a><a style=\"display:none\">" + accessoryEditInput + "</a>";
                  }
                  else
                  {
                      content += "<ul class='operation-Add' style='display:none;float:right'>" + operationEditAddInput + "</ul><ul class='operation-group' style='"+this.showOperation()+"'>" + operationInputEdit + "</ul><a style=\"display:none\">" + tid + "</a><a style=\"display:none\">" + partName + "</a><a style=\"display:none\">" + accessoryEditInput + "</a>";
                  }
                  break;
              case "Interface":
              case "Level":
              case "Viscosity":
              case "Description":
              case "Grade":
                  if (value == "" || value == "无数据") {
                      content += "<ul class='operation-group' style='float:right;"+this.showOperation()+"'>" + operationEditAddSelect + "</ul><ul class='operation-Add' style='display:none;float:right'>" + operationSelectEdit + "</ul><a style=\"display:none\">" + tid + "</a><a style=\"display:none\">" + partName + "</a><a>" + accessoryEditSelect + "</a><a style='float:right;display:none' class='selectsave-group'>" + operationBYSave + "</a>";
                  }
                  else {
                      content += "<ul class='operation-Add' style='display:none;float:right'>" + operationEditAddSelect + "</ul><ul class='operation-group' style='"+this.showOperation()+"'>" + operationSelectEdit + "</ul><a style=\"display:none\">" + tid + "</a><a style=\"display:none\">" + partName + "</a><a>" + accessoryEditSelect + "</a><a style='float:right;display:none' class='selectsave-group'>" + operationBYSave + "</a>";
                  }
                  break;
              default:
                  break;
          }
          return content;
        },
        noConvertEdit(key, tid, partName){
          var operationEditAddInput = "<li style='list-style-type:none;float:right' onclick='showEditBYInput(this)'><span class='ui-icon ui-icon-plus'></span></li>";
          var operationInputEdit = "<li style='list-style-type:none' onclick='showEditBYInput(this)'><span class='ui-icon ui-icon-pencil'></span></li>";
          var accessoryEditInput = "<input style='display:inline;width:50px;cursor:auto' type='text' onkeydown='submitEditAccessoryInfo(this)' onblur = 'hideEidtInput(this)'/>";
          var operationEditAddSelect = "<li onclick='showSelectEdit(this)' style='list-style-type:none;cursor:pointer;float:right'><span class='ui-icon ui-icon-plus'></span></li>";
          var operationSelectEdit = "<li  style='list-style-type:none' onclick='showSelectEdit(this)'><span class='ui-icon ui-icon-pencil'></span></li>";
          var accessoryEditSelect = "<select  class='selectEdit' style='display:none'><option values=''><option></select>";
          var operationBYSave = "<img style='cursor:pointer' src='"+require("../../assets/bao_yang_images/save.png")+"' onclick='saveSelectEditInfo(this)'style='display:inline'/>";
          var content = "";
          switch (key) {
              case "Volume":
              case "Quantity":
              case "Size":
                  content += "<ul class='operation-group' style='float:right;"+this.showOperation()+"'>" + operationEditAddInput + "</ul><ul class='operation-Add' style='display:none;float:right'>" + operationInputEdit + "</ul><a style=\"display:none\">" + tid + "</a><a style=\"display:none\">" + partName + "</a><a style=\"display:none\">" + accessoryEditInput + "</a>";
                  break;
              case "Interface":
              case "Level":
              case "Viscosity":
              case "Description":
              case "Grade":  
                  content += "<ul class='operation-group' style='float:right;"+this.showOperation()+"'>" + operationEditAddSelect + "</ul><ul class='operation-Add' style='display:none;float:right'>" + operationSelectEdit + "</ul><a style=\"display:none\">" + tid + "</a><a style=\"display:none\">" + partName + "</a><a>" + accessoryEditSelect + "</a><a style='float:right;display:none' class='selectsave-group'>" + operationBYSave + "</a>";
                  break;
              default:
                  break;
          }
          return content;
        },
        IsNull(value){
          if(value==="")
          {
              return true;
          }
          return false;
        },
        showPartAccessory(itemPart){
          var display="display:none";
          for (var i = 0; i < this.accessoryName.length; i++) {
              if (this.accessoryName[i] === itemPart) {
                  display="display:"
                  break;
              }
          }
          return display;
        },
        partAccessoryChange(){
          for (var x = 0; x < this.accessoryConfig.length; x++){
            var partName = this.accessoryConfig[x].partName;
            var sub_menu = document.getElementById('data_'+partName);
            if(this.accessoryName.indexOf(partName)>=0){
              if(sub_menu != null){
                sub_menu.style.display = "";
              }
            }
            else{
              if(sub_menu != null){
                sub_menu.style.display = "none";
              }
            }
          }
        },
        showEdit(){
          var editPart = document.getElementsByClassName("operation-group");
          var batchPart = document.getElementsByClassName("BatchAdd-group");
          for (var i = 0; i < editPart.length; i++){
            if(this.checked){
              editPart[i].style.display = "";
            }
            else{
              editPart[i].style.display = "none";
            }
          }

          for (var i = 0; i < batchPart.length; i++){
            if(this.checked){
              batchPart[i].style.display = "";
            }
            else{
              batchPart[i].style.display = "none";
            }
          }
        },
        showOperation(){
          var display="display:none";
          if(this.checked){
            display=""
          }
          return display;
        },
        checkInputValue(accessory, attributeName, attributevalue){
          var zInt = /^[0-9]*$/;
          var zFloat = /^(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*))$/;
          for(var i=0;i<this.accessoryConfig.length;i++){
            if (this.accessoryConfig[i].partName === accessory){
              if(attributeName == "Volume"){
                if (zFloat.test(attributevalue) || zInt.test(attributevalue)) {
                    if (parseFloat(attributevalue) > this.accessoryConfig[i].volume.minValue
                        && parseFloat(attributevalue) <= this.accessoryConfig[i].volume.maxValue) {
                        return "";
                    } else {
                        return "输入的数值不在有效范围内！";
                    }
                }
                else {
                    return "请输入数字！";
                }       
              }
              else if(attributeName == "Quantity"){
                if (zInt.test(attributevalue)) {
                    if (parseInt(attributevalue) >= this.accessoryConfig[i].quantity.minValue
                        && parseInt(attributevalue) <= this.accessoryConfig[i].quantity.maxValue) {
                        return "";
                    } else {
                        return "输入的数值不在有效范围内！";
                    }
                }
                else {
                    return "请输入数字且必须为整数！";
                }
              }
            }
          }
          return "";
        },
        showEditBYInput(o){
          var tdElement = o.parentElement.parentElement.childNodes;
          var oldElement = tdElement[2];
          var inputElement = tdElement[7].childNodes[0];
          if(oldElement.text !== "无数据"){
            inputElement.value = oldElement.text;
          }
          oldElement.style.display = "none";
          tdElement[7].style.display = "";
          inputElement.focus();
          var tid = tdElement[5].text;
          var partName = tdElement[6].text;
          document.getElementById("Unit"+tid+partName).style.display = "";
        },
        submitEditAccessoryInfo(o){
          if (event.keyCode == 13){
            var childNodes = o.parentElement.parentElement.childNodes;
            var tid = childNodes[5].text;
            var accessoryName = childNodes[6].text;
            var attributeName = childNodes[0].attributes["key"].nodeValue;
            var attributeValue = event.target.value;
            var submitRequest = {
              tid:tid, 
              accessoryName:accessoryName,
              attributeName:attributeName,
              attributeValue:attributeValue
            };
            var dataCheck = this.checkInputValue(accessoryName,attributeName,attributeValue);
            if(dataCheck != ""){
              this.$message({message: dataCheck,type: "error"});
              return;
            }
            appSvc.updateAccessory(submitRequest).then(res=>{
              var data = res.data;
              if(data){
                this.$message({message: "修改成功",type: "success"});
                if(attributeValue == "" || attributeValue ==0){
                  attributeValue = "无数据";
                }
                childNodes[2].text = attributeValue;
                childNodes[2].style.display = "";
                childNodes[7].style.display = "none";
                if(attributeValue !== "无数据")
                {
                  childNodes[3].style.display = "none";
                  childNodes[4].style.display = "";
                }
                else{
                  childNodes[3].style.display = "";
                  childNodes[4].style.display = "none";
                }
              }
              else{
                this.$message({message: "修改失败",type: "error"});
              }
            },error=>{
                console.log(error)
            })
            .catch(()=>{})
            .finally(()=>{})
          }
        },
        saveSelectEditInfo(o){
          var childNodes = o.parentElement.parentElement.childNodes;
          var tid = childNodes[5].text;
          var accessoryName = childNodes[6].text;
          var attributeName = childNodes[0].attributes["key"].nodeValue;
          var attributeValue = childNodes[7].childNodes[0].value;
          var submitRequest = {
            tid:tid, 
            accessoryName:accessoryName,
            attributeName:attributeName,
            attributeValue:attributeValue
          };
          appSvc.updateAccessory(submitRequest).then(res=>{
            var data = res.data;
            if(data){
              this.$message({message: "修改成功",type: "success"});
              if(attributeValue == "" || attributeValue ==0){
                attributeValue = "无数据";
              }
              childNodes[2].text = attributeValue;
              childNodes[2].style.display = "";
              childNodes[7].childNodes[0].style.display = "none";
              childNodes[8].style.display = "none"
              if(attributeValue !== "无数据")
              {
                childNodes[3].style.display = "none";
                childNodes[4].style.display = "";
              }
              else{
                childNodes[3].style.display = "";
                childNodes[4].style.display = "none";
              }
            }
            else{
              this.$message({message: "修改失败",type: "error"});
            }
          },error=>{
              console.log(error)
          })
          .catch(()=>{})
          .finally(()=>{})
        },
        hideEidtInput(o){
          var tdElement = o.parentElement.parentElement.childNodes;
          tdElement[2].style.display = "";
          tdElement[7].style.display = "none";
          var tid = tdElement[5].text;
          var partName = tdElement[6].text;
          var value = tdElement[2].text;
          if (value !== "无数据") {
            document.getElementById("Unit"+tid+partName).style.display = "";
          }
          else{
            document.getElementById("Unit"+tid+partName).style.display = "none";
          }
        },
        deleteData(o){
          this.$confirm('确定要删除吗?', '提示', {
          confirmButtonText: '确定',
          cancelButtonText: '取消',
          closeOnClickModal: false,
          type: 'warning'
          }).then(() => {
            var childNodes = o.parentElement;
            var tid = childNodes.attributes["tid"].nodeValue;
            var accessoryName = childNodes.attributes["accessory"].nodeValue;
            var deleteRequest = {
              tid:tid, 
              accessoryName:accessoryName
            };
            appSvc.deleteAccessory(deleteRequest).then(res=>{
                var data = res.data;
                if(data){
                   this.$message({message: "删除成功",type: "success"});
                   for(var i=0;i<this.accessoryConfig.length;i++){
                     if(this.accessoryConfig[i].partName == accessoryName){
                       for (var j = 0; j < this.accessoryConfig[i].display.length; j++){
                         var trElement = o.parentElement.parentElement.parentElement.parentElement.childNodes;
                         var childEmement = trElement[j].childNodes[0].childNodes;
                         childEmement[2].text = "无数据",
                         childEmement[3].style.display = "";
                         childEmement[4].style.display = "none";
                         document.getElementById("Unit"+tid+accessoryName).style.display = "none";
                       }
                       break;
                     }
                   }
                }
                else{
                  this.$message({message: "删除失败",type: "error"});
                }
            },error=>{
                console.log(error)
            })
            .catch(()=>{})
            .finally(()=>{})
          }).catch(()=>{});  
        },
        showSelectEdit(o){
          var childNodes = o.parentElement.parentElement.childNodes;
          var attributeName = childNodes[0].attributes["key"].nodeValue;
          var attributeValue = childNodes[2].text;
          if(childNodes[7].children[0].style.display == "none"){
            var selectElement = document.getElementsByClassName("selectEdit");
            var saveElement = document.getElementsByClassName("selectsave-group");
            var groupElement = document.getElementsByClassName("value-group");
            for(var i=0;i<selectElement.length;i++){
              selectElement[i].style.display = "none";
              selectElement[i].options.length = 0;
            }
            for(var i=0;i<saveElement.length;i++){
              saveElement[i].style.display = "none";
            }
            for(var i=0;i<groupElement.length;i++){
              groupElement[i].style.display = "";
            }

            childNodes[2].style.display = "none";
            childNodes[8].style.display = "";
            childNodes[7].childNodes[0].style.display = "";
          }
          else{
            childNodes[2].style.display = "";
            childNodes[8].style.display = "none";
            childNodes[7].childNodes[0].style.display = "none";
          }
          for(var i = 0;i<this.accessoryConfig.length;i++){
            if(this.accessoryConfig[i].partName == childNodes[6].text){
              childNodes[7].childNodes[0].options.add(new Option('-请输入选择-',''));
              if (attributeName == "Level"){
                var level = this.accessoryConfig[i].level.split(",");
                for (var j = 0; j < level.length; j++) {
                  var option = new Option(level[j],level[j]);
                  if(level[j] == attributeValue){
                    option.selected = true;
                  }
                  childNodes[7].childNodes[0].options.add(option);
                }
              }
              if (attributeName == "Interface"){
                var Interface = this.accessoryConfig[i].interface.split(",");
                for (var j = 0; j < Interface.length; j++) {
                  var option = new Option(Interface[j],Interface[j]);
                  if(Interface[j] == attributeValue){
                    option.selected = true;
                  }
                  childNodes[7].childNodes[0].options.add(option);
                }
              }
              if (attributeName == "Viscosity"){
                var viscosity = this.accessoryConfig[i].viscosity.split(",");
                for (var j = 0; j < viscosity.length; j++)
                {
                  var option = new Option(viscosity[j],viscosity[j]);
                  if(viscosity[j] == attributeValue){
                    option.selected = true;
                  }
                  childNodes[7].childNodes[0].options.add(option);
                }
              }
              if (attributeName == "Description") {
                var description = this.accessoryConfig[i].description.split(",");
                for (var j = 0; j < description.length; j++) {
                  var option = new Option(description[j],description[j]);
                  if(description[j] == attributeValue){
                    option.selected = true;
                  }
                  childNodes[7].childNodes[0].options.add(option);
                }
              }
              if (attributeName == "Grade") {
                var grade = this.accessoryConfig[i].grade.split(",");
                for (var j = 0; j < grade.length; j++) {
                  var option = new Option(grade[j],grade[j]);
                  if(grade[j] == attributeValue){
                    option.selected = true;
                  }
                  childNodes[7].childNodes[0].options.add(option);
                }
              }
              break;
            }
          }
        },
        editAccessories(o){
          var childNodes = o.parentElement.parentElement.childNodes;
          var partName = childNodes[0].innerText;
          this.currentPartAccessory = partName;
          this.addTid = [];
          var content = "";
          for (var index = 0; index < this.accessoryConfig.length; index++){
            if (this.accessoryConfig[index].partName == partName){
              for (var j = 0; j < this.accessoryConfig[index].display.length; j++){
                var key = this.accessoryConfig[index].display[j].key;
                content += "<tr><td><span>" + this.accessoryConfig[index].display[j].value + "</span>："
                var option = "<option value = ''>-请输入选择-</option>";
                switch (key){
                  case "Volume":
                    content += "<input id='" + key + "' name='dialogGroup' keyword='"+key+"'  type='text' style='width:107px;'/>";
                    content += "&nbsp;<span>" + this.accessoryConfig[index].volume.unit + "</span>";
                    break;
                  case "Quantity":
                    content += "<input id='" + key + "' name='dialogGroup' keyword='"+key+"'  type='text'/>";
                    content += "&nbsp;<span>" + this.accessoryConfig[index].quantity.unit + "</span>";
                    break;
                  case "Size":
                    content += "<input id='" + key + "' name='dialogGroup' keyword='"+key+"'  type='text'/>";
                    content += "&nbsp;<span>" + this.accessoryConfig[index].size.unit + "</span>";
                    break;
                  case "Interface":
                    var Interface = this.accessoryConfig[index].interface.split(",");
                    for (var x = 0; x < Interface.length; x++) {
                      option += "<option value = '"+Interface[x]+"'>"+Interface[x]+"</option>";
                    }
                    content += "<select id='" + key + "' name='dialogGroup' keyword='" + key + "'>"+ option +"</select>";
                    break;
                  case "Level":
                    var level = this.accessoryConfig[index].level.split(",");
                    for (var x = 0; x < level.length; x++){
                      option += "<option value = '"+level[x]+"'>"+level[x]+"</option>";
                    }
                    content += "<select id='" + key + "' name='dialogGroup' keyword='" + key + "'>"+ option +"</select>";
                    break;
                  case "Viscosity":
                    var viscosity = this.accessoryConfig[index].viscosity.split(",");
                    for (var x = 0; x < viscosity.length; x++){
                      option += "<option value = '"+viscosity[x]+"'>"+viscosity[x]+"</option>";
                    }
                    content += "<select id='" + key + "' name='dialogGroup' keyword='" + key + "'>"+ option +"</select>";
                    break;
                  case "Description":
                    var description = this.accessoryConfig[index].description.split(",");
                    for (var x = 0; x < description.length; x++){
                      option += "<option value = '"+description[x]+"'>"+description[x]+"</option>";
                    }
                    content += "<select id='" + key + "' name='dialogGroup' keyword='" + key + "'>"+ option +"</select>";
                    break;
                  case "Grade":
                    var grade = this.accessoryConfig[index].grade.split(",");
                    for (var x = 0; x < grade.length; x++){
                      option += "<option value = '"+grade[x]+"'>"+grade[x]+"</option>";
                    }
                    content += "<select id='" + key + "' name='dialogGroup' keyword='" + key + "'>"+ option +"</select>";
                    break;
                  default:
                    break;
                }
                content += "</td></tr>";
              }
              break;
            }
          }
          this.innerHtmlAttribute = content;
          this.dialogFormVisible = true;
        },
        batchEditAccessory(){
          debugger;
          if(this.addTid.length==0){
            this.$message({message: "请选择TID",type: "warning"});
            return;
          }
          var attributeElement = document.getElementsByName("dialogGroup");
          var errMsg = "";
          var attributeList = new Array();
          for(var i=0;i<attributeElement.length;i++){
            var name = attributeElement[i].id;
            var value = attributeElement[i].value;
            if(name == "Volume"||name =="Quantity"||name=="Size"){
              errMsg = this.checkInputValue(this.currentPartAccessory,name,value);
              if(errMsg !==""){
                break;
              }
            }
            var attributeItem={attributeName:name,attributeValue:value};
            attributeList.push(attributeItem);
          }
          if(errMsg!==""){
            this.$message({message: errMsg,type: "warning"});
            return;
          }
          var batchEditRequest = {
            tidList:this.addTid,
            accessoryName:this.currentPartAccessory,
            attribute:attributeList
          };
          appSvc.batchEditAccessory(batchEditRequest).then(res=>{
                var data = res.data;
                if(data){
                  this.$message({message: "修改成功",type: "success"});
                  this.dialogFormVisible = false;
                  this.innerHtmlAttribute = "";
                  this.getBaoYangPartAccessory();
                }
                else{
                  this.$message({message: "修改失败",type: "error"});
                }
            },error=>{
                console.log(error)
            })
            .catch(()=>{})
            .finally(()=>{})
        }
    }
}
</script>


<style lang="scss" scoped>
.input{
    width: 200px;
}
.container {
  margin-left: 30px;
.input-label{
    margin-left:4px;
    font-size:14px;
    }
  }
.buttons{
    margin-top:20px;
   
}
.box{
    //display:flex;
    align-items: center;
 .but{
  margin-left:550px;  
    height:30px;
    width:80px;
 }
}
.el-tabs__header {
      background-color: aliceblue;
      }    
.el-dialog__header{
    margin-top:6px;
    background-color: rgb(96, 178, 250);
    border-radius: 10px;
    height:35px;
    padding:6px;

}
/deep/ .el-dialog__body{
  padding: 5px 20px !important;
}
.el-dialog__headerbtn{
    top:10px;
   
}
.el-select__tags{
    max-width:500px;
}
.el-dialog__title{
color:white;
font-size:14px;
}
.el-form-item__label{
   margin-top:10px;
    background-color:#eee;
    text-align:left;
    width:100px;
    padding:0 0 0 20px ;
}

.tableBox{
      margin-top: 18px;
      margin-bottom: 18px;
  th{
        height: 32px;
    background: #66ABEF;
    color: #F3F7FA;
    border: 1px solid #559BD0;
    font-weight: 600;
    font-family: "Microsoft Yahei";
    text-align: center;
    min-width: 250px;
    
    padding: 6px 5px;
  }
  td{
font-size: 75%;
    padding: 8px 3px 8px 3px;
    font-family: "Microsoft Yahei";
    text-align: center;
    border: 1px solid #eee;
    min-width: 250px;
    display: table-cell;
    vertical-align: inherit;
  }

}

.clearPadding {
    padding: 0 !important;
    vertical-align: top;
    border: 2px solid black !important;
}
.oeCode {
    color: #08c;
    font-weight: bolder;
}
.bage {
    display: inline-block;
    min-width: 10px;
    padding: 3px 7px;
    font-size: 12px;
    font-weight: 700;
    line-height: 1;
    color: #fff;
    text-align: center;
    white-space: nowrap;
    vertical-align: baseline;
    border-radius: 10px;
}
.bage-noproduct {
    background-color: #3c763d;
}

/deep/ .operation-group {
    list-style: none;
    padding: 0;
    margin: 0;
    float: right;
}

/deep/ .operation-Add {
    list-style: none;
    padding-left: 0;
    margin: 0;
}

/deep/ .operation-group li {
    float: right;
    cursor: pointer;
    margin-left: 5px;
    padding: 3px;
}

/deep/ .operation-Add li {
    float: right;
    cursor: pointer;
    margin-left: 5px;
    padding: 3px;
}

.tableContainer {
    width: auto;
}

/deep/ .tableContainer th {
    height: 32px;
    font-size: 75%;
    background: #66ABEF;
    color: #F3F7FA;
    border: 1px solid #559BD0;
    font-weight: 600 !important;
    font-family: "Microsoft Yahei";
    text-align: center;
    min-width: 250px;
}

/deep/ .tableContainer td {
    font-size: 75%;
    padding: 8px 3px 8px 3px;
    font-family: "Microsoft Yahei";
    text-align: center;
    border: 1px solid #eee;
    min-width: 250px;
}

/deep/ .operation-group .ui-state-error .operation-Add{
    border: 0;
}

/deep/ .clearPadding {
    padding: 0 !important;
    vertical-align: top;
    border: 2px solid black !important;
}

/deep/ .value-group{
  color: #08c
}

/deep/ a{
  color: #08c
}

/deep/ .innerTable {
    width: 100%;
    th{
      height: 32px;
      background: #66ABEF;
      color: #F3F7FA;
      border: 1px solid #559BD0;
      font-weight: 600;
      font-family: "Microsoft Yahei";
      text-align: left;
    }
    td{
      font-size: 75%;
      font-family: "Microsoft Yahei";
      text-align: left;
      border: 1px solid #eee;
    }
}

.table-responsive {
  overflow-x: auto;
}

/deep/ table{
    border: 0;
    border-collapse: collapse;
    clear: both;
}

/deep/ .ui-icon{
  width: 16px;
  height: 16px;
  background-image: url("../../assets/bao_yang_images/ui-icons_469bdd_256x240.png");
  display: block;
}

/deep/ .ui-state-error .ui-icon{
  background-image: url("../../assets/bao_yang_images/ui-icons_cd0a0a_256x240.png");
}

/deep/ .ui-state-error{
  background-color: #fef1ec;
}

/deep/ .ui-icon-pencil{
  background-position:-64px -112px;
}

/deep/ .ui-icon-trash{
    background-position:-176px -96px;
}

/deep/ .ui-icon-plus{
  background-position:-16px -128px;
}

/deep/ .dialog-addAttribute{
  width: 100%;
  tr{
    height: 36px;
  }
  td{
    padding: 5px;
    border: solid 1px #e8eef4
  }
}
</style>