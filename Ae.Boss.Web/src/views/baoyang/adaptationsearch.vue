<template>
  <main class="container">
    <div class="topBar" style="margin-top:28px;">
      <el-tabs type="border-card" v-model="activeName">
        <el-tab-pane label="选择车型" name="1">
        <div class="carSelect">
          <span class="input-label"> 品牌</span>
          <el-select v-model="brand" @change="getVehicleByBrand();" filterable placeholder="-请选择品牌-" size="small">
              <el-option
              v-for="item in brandList"
              :key="item.brand"
              :label="item.brand"
              :value="item.brand">
              </el-option>
          </el-select>
          <span class="input-label">车系</span>
          <el-select v-model="vehicleId" @change="getPaiLiangByVehicleId()" filterable placeholder="-请选择车系-" size="small">
              <el-option
              v-for="item in vehicleList"
              :key="item.vehicleId"
              :label="item.vehicle"
              :value="item.vehicleId">
              </el-option>
          </el-select>
          <span class="input-label"> 排量</span>
          <el-select v-model="paiLiang" @change="getVehicleNianByPaiLiang()" filterable placeholder="-请选择排量-" size="small">
              <el-option
              v-for="item in paiLiangList"
              :key="item"
              :label="item"
              :value="item">
              </el-option>
          </el-select><span style="color:red;font-size:12px;margin-left:3px;">(*必选)</span>
          <br/> <br/>
          <span class="input-label"> 生产年份</span>
          <el-select v-model="nian" @clear="tid='';salesNameList=[];" @change="getVehicleSalesNameByNian()" clearable filterable placeholder="-请选择生产年份-" size="small">
              <el-option
              v-for="item in nianList"
              :key="item"
              :label="item"
              :value="item">
              </el-option>
          </el-select>
          <span class="input-label"> 车款</span>
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
                  <span class="input-label"> 输入VehicleID</span>
                  <el-input class="input" clearable size="small" placeholder="请输入VID" @change="getPaiLiangByVehicleIdV2()" v-model="secondVehicleId"></el-input>
                  <span class="input-label"> 排量</span>
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
              <span class="input-label"> 输入TID</span>
              <el-input class="input" clearable size="small" placeholder="TID" v-model="secondTid"></el-input>
          </el-tab-pane>
      </el-tabs>
    </div>
<div class="buttons">
<el-row>
  <el-button type="success" size="small" @click="getBaoYangPartAdaptations()">查询</el-button>
  <el-button type="primary" size="small" @click="buikAddPartCode()">批量添加</el-button>
</el-row>
</div>
<div class="box">
  <el-select v-model="partNameList" multiple filterable placeholder="选择需要显示的配件" @change="partNameChange()" style="margin-top:10px;width:500px;" size="small">
        <el-option
        v-for="item in partNameConfig"
        :key="item.displayName"
        :label="item.displayName"
        :value="item.displayName"></el-option>
    </el-select>
  <el-checkbox style="float:right;font-weight:bold;margin-right:5px;margin-top:17px;" v-model="checked" @change="showEdit()">修改数据</el-checkbox>
  <el-dialog :width="addDialogWidth" style="margin-top: 10vh" title="添加零件号" :close-on-click-modal='false' :visible.sync="dialogFormVisible">
     <div class="form">
        <div>
            <label>Tid</label><span id="addPC-tid">{{this.addPartRequest.tid}}</span>
        </div>
        <div>
            <label>零件名称:</label><span id="addPC-partName">{{this.addPartRequest.partName}}</span>
        </div>
        <div>
            <label>OE件号:</label><span id="addPC-oeCode">{{this.addPartRequest.oeCode}}</span>
        </div>
        <div>
            <label>零件号:</label><el-input id="inputPartCode" style="width:180px;margin-left:9px;" size="small" placeholder="请输入零件号" v-model="addPartRequest.partCode"></el-input>
        </div>
        <div>
            <label>品牌</label>
            <el-select style="width:180px;" v-model="addPartRequest.brand" filterable placeholder="-请选择品牌-" size="small">
              <el-option
              v-for="item in partCodeBrandList"
              :key="item"
              :label="item"
              :value="item">
              </el-option>
            </el-select>
        </div>
    </div>
    <span slot="footer" class="dialog-footer">
      <el-button type="primary" @click="submitAddPartCode();">提 交</el-button>
      <el-button @click="dialogFormVisible = false">取 消</el-button>
    </span>
  </el-dialog>

  <el-dialog :width="addDialogWidth" style="margin-top: 10vh" title="添加零件号" :close-on-click-modal='false' :visible.sync="dialogSpecialFormVisible">
    <div class="form">
      <div>
        <label>Tid</label><span id="addWiperPC-tid">{{this.addSpecialPartRequest.tid}}</span>
      </div>
      <div>
        <label>零件名称</label><span id="addWiperPC-partName">{{this.addSpecialPartRequest.partName}}</span>
      </div>
      <div>
        <label>零件号</label><el-input id="inputSpecialPartCode" style="width:180px;margin-left:9px;" size="small" placeholder="请输入零件号" v-model="addSpecialPartRequest.partCode"></el-input>
      </div>
      <div>
        <label>品牌</label>
        <el-select style="width:180px;" v-model="addSpecialPartRequest.brand" clearable filterable placeholder="-请选择品牌-" size="small">
          <el-option
          v-for="item in partCodeBrandList"
          :key="item"
          :label="item"
          :value="item">
          </el-option>
        </el-select>
      </div>
      <div>
        <label>种类</label>
        <el-select style="width:180px;" v-model="addSpecialPartRequest.partType" filterable placeholder="-请选择种类-" size="small">
          <el-option
          v-for="item in partTypeList"
          :key="item"
          :label="item"
          :value="item">
          </el-option>
        </el-select>
      </div>
    </div>
    <span slot="footer" class="dialog-footer">
      <el-button type="primary" @click="submitAddSpecialPartCode();">提 交</el-button>
      <el-button @click="dialogSpecialFormVisible = false">取 消</el-button>
    </span>
  </el-dialog>

  <el-dialog :before-close="closeDialog" class="abow_dialog" :width="dialogWidthV2" :title="dialogTitle" :close-on-click-modal='false' :visible.sync="dialogVisibleV1">
    <table style="width:100%" class="dialogTable">
      <tr>
        <th><label>项目</label></th>
        <td style="margin-left:8px;">
          <el-select style="width:80%;" @change="changePartCode()" v-model="selectPartList" multiple filterable placeholder="选择配件" size="small" :clearable="clearable" :disabled="disable">
            <el-option
            v-for="item in partNameConfig"
            :key="item.displayName"
            :label="item.displayName"
            :value="item.displayName">
            </el-option>
          </el-select>
        </td>
      </tr>
      <tr>
        <th><label>Tid</label></th>
        <td style="margin-left:8px;">
          <el-select style="width:80%;" v-model="selectTidList" multiple clearable filterable placeholder="选择需要添加的TID" size="small">
            <el-checkbox style="margin-left:10px;padding:6px;" v-model="tidChecked" @change='selectAll'>全选</el-checkbox>
            <el-option
            v-for="item in partAdaptationList"
            :key="item.tid"
            :label="item.tid"
            :value="item.tid">
            </el-option>
          </el-select>
        </td>
      </tr>
    </table>
    <div name="dataPartHtml" style="overflow: auto" v-html="this.innerHtmlAttribute">
    </div>
    <span slot="footer" class="dialog-footer">
      <el-button type="primary" @click="addPartCodeCommon();">提 交</el-button>
      <el-button @click="closeDialog();">取 消</el-button>
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
  name:'adaptationsearch',
        data() {
      return {
        addPartRequest:{
          tid:'',
          partName:'',
          oeCode:'',
          partCode:'',
          brand:''
        },
        addSpecialPartRequest:{
          tid:'',
          partName:'',
          partCode:'',
          brand:'',
          partType:''
        },
        selectPartList:[],
        selectTidList:[],
        clearable:false,
        disable:false,
        dialogTitle:'',
        partCodeBrandList:[],
        partTypeList:[],
        addDialogWidth:"360px",
        dialogWidthV2:"600px",
        dialogFormVisible:false,
        dialogSpecialFormVisible:false,
        dialogVisibleV1:false,
        tableLoading:false,
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
        partNameList:[],
        partNameConfig:[],
        partAdaptationList:[],
        innerHtmlData:'',
        innerHtmlAttribute:'',
        tidChecked:false//下拉框全选
      }
   
      
    },
    created(){
      this.getBrandList();
      this.getPartNameList();
      window.showPid = this.showPid;
      window.nohide = this.nohide;
      window.hidePid = this.hidePid;
      window.canhide = this.canhide;
      window.showEditPCArea = this.showEditPCArea;
      window.hideEidtPCArea = this.hideEidtPCArea;
      window.submitEditPC = this.submitEditPC;
      window.showDeletePCConfirm = this.showDeletePCConfirm;
      window.addPartCode = this.addPartCode;
      window.showEditOeArea = this.showEditOeArea;
      window.hideEidtOeArea = this.hideEidtOeArea;
      window.submitEditOe = this.submitEditOe;
      window.showDeleteOeConfirm = this.showDeleteOeConfirm;
      window.showDeleteSpecialPartConfirm = this.showDeleteSpecialPartConfirm;
      window.addSpecialPartCode = this.addSpecialPartCode;
      window.addOEAndMapping = this.addOEAndMapping;
      window.addPCArea = this.addPCArea;
      window.removePCArea = this.removePCArea;
      window.addSpecialPCArea = this.addSpecialPCArea;
      window.loadOeMapping = this.loadOeMapping;
      window.deleteAllOEs = this.deleteAllOEs;
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
        getPartNameList(){
          appSvc.getPartNameList().then(
            res => {
                var data = res.data;
                this.partNameConfig = data;
            },
            error => {
                console.log(error)
            }
            )
            .catch(() => {})
            .finally(() => {});
        },
        getBaoYangPartAdaptations(){
          var tabType = this.activeName;
          var partRequest={
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
              partRequest.vehicleId=this.vehicleId;
              partRequest.paiLiang=this.paiLiang;
              partRequest.nian=this.nian;
              partRequest.tid=this.tid;
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
              partRequest.vehicleId=this.secondVehicleId;
              partRequest.paiLiang=this.secondPaiLiang;
          }
          else if(tabType=="3"){
              if(this.secondTid==''){
                  this.$message({message: "请输入Tid",type: "error"});
                  return;
              }
              partRequest.tid=this.secondTid;
          }
          else{
              return;
          }
          this.tableLoading = true;
          console.log("condition: " + JSON.stringify(partRequest));
          appSvc.getBaoYangPartAdaptations(partRequest).then(res=>{
              var data = res.data;
              this.partAdaptationList = data;
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
          var data = this.partAdaptationList;
          var partNames = this.partNameConfig;
          var html = "";
          var rowTid = "<tr><th>TID</th>";
          var rowVehicleID = "<tr><td style='font-weight:bolder'>VehicleID</td>";
          var rowVehicleName = "<tr><td style='font-weight:bolder'>车型名称</td>";
          var rowFdjName = "<tr><td style='font-weight:bolder'>发动机号</td>";
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
            var currentPart = partNames[index];
            var partItem = currentPart.displayName;
            html += "<tr id='data_" + partItem + "' style='"+this.showPartAccessory(partItem)+"'><td style='font-weight:bolder'>" + partItem + "</td>";
            for (var i = 0; i < data.length; i++){
              var tid = data[i].tid;
              var details = data[i].partsAdaptation;
              var specials = data[i].partsSpecialAdaptations;
              var content = "";
              if(details != null){
                for (var j = 0; j < details.length; j++){
                  if(details[j].partDisplayName == partItem){
                    content = this.convertOEDetailToHtml(tid, partItem, details[j].oeCodeAdaptationDetails);
                  }
                }
              }
              if(specials != null){
                for (var j = 0; j < specials.length; j++) {
                  var special = specials[j]; 
                  if (special.partDisplayName == partItem) {
                      content = this.convertSpecialDetailToHtml(tid, partItem, special.specialAdaptationParts);
                  }
                }
              }
              if(currentPart.isSpecialPart){
                if (content == "") {
                  content += "<td id='" + tid + partItem + "'><span isNoData='isNoData' style='"+this.hideOperation()+"'>无数据</span><div isOpeartion='isOpeartion' onclick='addOEAndMapping(this)' partName='" + partItem + "' tid='" + tid + "' style='cursor:pointer;"+this.showOperation()+"' class='operation-hover'><span class='ui-icon ui-icon-plus' style='display: inline-block !important;'></span><span>添加" + partItem + "</span></div></td>";
                } else {
                  content = "<td id='" + tid + partItem + "' class='clearPadding'><table class='innerTable'>" + content + "<tr isOpeartion='isOpeartion' style='"+this.showOperation()+"'><td style='padding: 0'><ul class='operation-Add'><li onclick='addOEAndMapping(this)' partName='" + partItem + "' tid='" + tid + "' style='width: 50%;float: left;border-right: 1px solid;'>添加" + partItem + "</li><li onclick='deleteAllOEs(this)' partName='" + partItem + "' tid='" + tid + "'>删除所有" + partItem + "</li></ul></td></tr></table></td>";
                }
              }
              else{
                if (content == "") {
                  content += "<td id='" + tid + partItem + "'><span isNoData='isNoData' style='"+this.hideOperation()+"'>无数据</span><div isOpeartion='isOpeartion' onclick='addOEAndMapping(this)' partName='" + partItem + "' tid='" + tid + "' style='cursor:pointer;"+this.showOperation()+"' class='operation-hover'><span class='ui-icon ui-icon-plus' style='display: inline-block !important;'></span><span>添加OE号</span></div></td>";
                } else {
                  content = "<td id='" + tid + partItem + "' class='clearPadding'><table class='innerTable'>" + content + "<tr isOpeartion='isOpeartion' style='"+this.showOperation()+"'><td style='padding: 0'><ul class='operation-Add'><li onclick='addOEAndMapping(this)' partName='" + partItem + "' tid='" + tid + "' style='width: 50%;float: left;border-right: 1px solid;'>添加OE号</li><li onclick='deleteAllOEs(this)' partName='" + partItem + "' tid='" + tid + "'>删除所有OE号</li></ul></td></tr></table></td>";
                }
              }
              html += content;
            }
            html += "</tr>";
          }
          this.innerHtmlData = html;
        },
        convertOEDetailToHtml(tid,partName,oeDetails){
          var oeEditInput = "<input type='text' style='display: none;width:90px;' onkeydown='submitEditOe(this)' onblur = 'hideEidtOeArea(this)'/>";
          var operationDelete = "<li class='ui-state-error' onclick='showDeleteOeConfirm(this)'><span class='ui-icon ui-icon-trash'></span></li>";
          var operationEdit = "<li onclick='showEditOeArea(this)'><span class='ui-icon ui-icon-pencil'></span></li>";
          var operationPAdd = "<li onclick='addPartCode(this)'><span class='ui-icon ui-icon-plus'></span></li>";
          var oeAddInput = "<input type='text' style='display: none' onkeydown='submitAddOe(this)' onblur = 'hideEidtOeArea(this)'/>";
          var operationAdd = "<li onclick='showEditOeArea(this)'><span class='ui-icon ui-icon-pencil'></span></li>";
          var operationPDelete = "<li class='ui-state-error' onclick='showDeletePCConfirm(this)'><span class='ui-icon ui-icon-trash'></span></li>";
          var operationPEdit = "<li onclick='showEditPCArea(this)'><span class='ui-icon ui-icon-pencil'></span></li>";
          var pEditInput = "<input type='text' style='display: none;width:85px;' onkeydown='submitEditPC(this)' onblur = 'hideEidtPCArea(this)'/>";
          var content = "";
          for (var s = 0; s < oeDetails.length; s++){
            content += "<tr isOeCode='isOeCode'>";
            if (oeDetails[s].oePartCode !== ""){
              content += "<td><span>OE件号：</span>" + oeEditInput + "<span class='oeCode' tid='" + tid + "' partName='" + partName + "'>" + oeDetails[s].oePartCode + "</span><ul class='operation-group' style='"+this.showOperation()+"'>" + operationDelete + operationEdit + operationPAdd + "</ul></td>";
            }
            else{
              content += "<td><span>OE件号：</span>" + oeAddInput + "<span class='oeCode' tid='" + tid + "' partName='" + partName + "'>" + oeDetails[s].oePartCode + "</span><ul class='operation-group' style='"+this.showOperation()+"'>" + operationDelete + operationAdd + operationPAdd + "</ul></td>";
            }
            content += "</tr>";
            var partDetails = oeDetails[s].partCodeAdaptation;
            for (var t = 0; t < partDetails.length; t++){
              if (partDetails[t].partCode !== ""){
                var hasBrand = 0;
                for (var i = 0; i< t; i++) {
                  if (partDetails[t].brand === partDetails[i].brand && partDetails[i].partCode !== "") {
                      hasBrand++;
                  }
                }
                if (t == 0 || hasBrand == 0) {
                  content += "<tr isBrand = 'isBrand'>";
                  content += "<td><span>" + partDetails[t].brand + "</span></td></tr>";
                }
                content += "<tr rowStatus = '" + partDetails[t].status + "'><td><span>&nbsp;&nbsp;&nbsp;&nbsp;零件号：</span><ul class='operation-group' style='"+this.showOperation()+"'>" + operationPDelete + operationPEdit + "</ul>";
                if (partDetails[t].status == 2 || partDetails[t].status == 4) {
                    var productHtml = "";
                    var products = partDetails[t].partsProducts;
                    for (var m = 0; m < products.length; m++) {
                        var pid = products[m].productPid;
                        if (products[m].isOnSale && !products[m].isStockOut) {
                            productHtml += "<a style='margin-left:5px'>" + pid + "</a>";
                        } else {
                            productHtml += "<a style='margin-left:5px; color:red'>" + pid + "</a>";
                        }
                    }
                    if (partDetails[t].status == 2) {
                        content += pEditInput + "<span status='" + partDetails[t].status + "' tid='" + tid + "'  pkid= '" + partDetails[t].id + "' partName='" + partName + "' style=' position: relative;' onmouseover='showPid(this)' onmouseout='hidePid(this)' class='bage bage-primary'>" + partDetails[t].partCode + "<span class='tooltip bage bage-tooltip' onmouseover='nohide(this)' onmouseout='canhide(this)' style='display:none'>" + productHtml + "</span></span>";
                    } else {
                        content += pEditInput + "<span status='" + partDetails[t].status + "' tid='" + tid + "'  pkid= '" + partDetails[t].id + "' partName='" + partName + "' style=' position: relative;' onmouseover='showPid(this)' onmouseout='hidePid(this)' class='bage bage-notavailable'>" + partDetails[t].partCode + "<span class='tooltip bage bage-tooltipnotavailable' onmouseover='nohide(this)' onmouseout='canhide(this)' style='display:none'>" + productHtml + "</span></span>";
                    }
                } else {
                    content += pEditInput + "<span status='" + partDetails[t].status + "' tid='" + tid + "'  pkid= '" + partDetails[t].id + "' partName='" + partName + "' class='bage bage-noproduct'>" + partDetails[t].partCode + "</span>";
                }
                content += "</td></tr>";
              }
            }
          }
          return content;
        },
        convertSpecialDetailToHtml(tid,partName,details){
          var operationSpecialPartAdd = "<li onclick='addSpecialPartCode(this)'><span class='ui-icon ui-icon-plus'></span></li>";
          var operationSpecialPartDelete = "<li class='ui-state-error' onclick='showDeleteSpecialPartConfirm(this)'><span class='ui-icon ui-icon-trash'></span></li>";
          var operationPDelete = "<li class='ui-state-error' onclick='showDeletePCConfirm(this)'><span class='ui-icon ui-icon-trash'></span></li>";
          var operationPEdit = "<li onclick='showEditPCArea(this)'><span class='ui-icon ui-icon-pencil'></span></li>";
          var pEditInput = "<input type='text' style='display: none;width:85px;' onkeydown='submitEditPC(this)' onblur = 'hideEidtPCArea(this)'/>";
          var content = "";
          for (var s = 0; s < details.length; s++){
            var name = details[s].partType;
            var partDetails = details[s].partCodeAdaptation;
            content += "<tr tid='" + tid + "'>";
            content += "<td><span>" + name + "</span><ul class='operation-group' style='"+this.showOperation()+"'>" + operationSpecialPartDelete + operationSpecialPartAdd + "</ul></td></tr>";
            for (var t = 0; t < partDetails.length; t++){
              if (t == 0 || partDetails[t].brand != partDetails[t - 1].brand){
                content += "<tr isBrand = 'isBrand'>";
                if (partDetails[t].brand != "") {
                    content += "<td><span>&nbsp;&nbsp;&nbsp;&nbsp;" + partDetails[t].brand + "</span></td>";
                }
                content += "</tr>";
              }
              if(partDetails[t].partCode != ""){
                if (partDetails[t].brand != ""){
                  content += "<tr rowStatus = '" + partDetails[t].status + "'><td><span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;零件号：</span><ul class='operation-group' style='"+this.showOperation()+"'>" + operationPDelete + operationPEdit + "</ul>";
                }
                else{
                  content += "<tr rowStatus = '" + partDetails[t].status + "'><td><span>&nbsp;&nbsp;&nbsp;&nbsp;零件号：</span><ul class='operation-group' style='"+this.showOperation()+"'>" + operationPDelete + operationPEdit + "</ul>";
                }
                if (partDetails[t].status == 2 || partDetails[t].status == 4){
                  var productHtml = "";
                  var products = partDetails[t].partsProducts;
                  for (var m = 0; m < products.length; m++) {
                    var pid = products[m].productPid;
                    if (products[m].isOnSale && !products[m].isStockOut) {
                        productHtml += "<a style='margin-left:5px'>" + pid + "</a>";
                    } else {
                        productHtml += "<a style='margin-left:5px; color:red'>" + pid + "</a>";
                    }
                  }
                  if (partDetails[t].status == 2) {
                    content += pEditInput + "<span status='" + partDetails[t].status + "' tid='" + tid + "'  pkid= '" + partDetails[t].id + "' isSpecial = 'isSpecial'  partName='" + name + "' onmouseover='showPid(this)' onmouseout='hidePid(this)' style='  position: relative;' class='bage bage-primary'>" + partDetails[t].partCode + "<span class='tooltip bage bage-tooltip' onmouseover='nohide(this)' onmouseout='canhide(this)'>" + productHtml + "</span></span>";
                  } else {
                    content += pEditInput + "<span status='" + partDetails[t].status + "' tid='" + tid + "'  pkid= '" + partDetails[t].id + "' isSpecial = 'isSpecial'  partName='" + name + "' onmouseover='showPid(this)' onmouseout='hidePid(this)' style='  position: relative;' class='bage bage-notavailable'>" + partDetails[t].partCode + "<span class='tooltip bage bage-tooltipnotavailable' onmouseover='nohide(this)' onmouseout='canhide(this)'>" + productHtml + "</span></span>";
                  }
                }
                else{
                  content += pEditInput + "<span status='" + partDetails[t].status + "' tid='" + tid + "'  pkid= '" + partDetails[t].id + "' isSpecial = 'isSpecial' partName='" + name + "' class='bage bage-noproduct'>" + partDetails[t].partCode + "</span>";
                }
                content += "</td></tr>";
              }
            }
          }

          return content;
        },
        showPartAccessory(itemPart){
          var display="display:none";
          for (var i = 0; i < this.partNameList.length; i++) {
              if (this.partNameList[i] === itemPart) {
                  display="display:"
                  break;
              }
          }
          return display;
        },
        partNameChange(){
          for (var x = 0; x < this.partNameConfig.length; x++){
            var partName = this.partNameConfig[x].displayName;
            var sub_menu = document.getElementById('data_'+partName);
            if(this.partNameList.indexOf(partName)>=0){
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
        showPid(o){
          var box_child = o.childNodes;
          for(var j = 0; j < box_child.length; j++){
            if(box_child[j].nodeType == 1){
              box_child[j].style.display = "";//判断这种元素是否为元素节点,如果当前节点的nodeType不等于1，就会删除当前节点，否则保留， 元素节点:nodeType=1,属性节点:nodeTyp=2,文本节点:nodeType=3 
            }   
          }
        },
        nohide(o){
          o.allowhide = "false";
        },
        hidePid(o){
          setTimeout(function () {
            for(var j = 0; j < o.childNodes.length; j++){
              if(o.childNodes[j].nodeType == 1 && o.childNodes[j].allowhide !== "false"){
                o.childNodes[j].style.display="none";
              }  
            }
          }, 100);
        },
        canhide(o){
          o.allowhide = "true";
          o.style.display="none";
        },
        showEdit(){
          var editPart = document.getElementsByClassName("operation-group");
          var noDataBtn = this.getDom('div','isOpeartion','isOpeartion');
          var noDataBtn2 = this.getDom('tr','isOpeartion','isOpeartion');
          var noDataText = this.getDom('span','isNoData','isNoData');
          if(this.checked){
            for (var i = 0; i < editPart.length; i++){
              editPart[i].style.display = "";
            }
            for (var i = 0; i < noDataBtn.length; i++){
              noDataBtn[i].style.display = "";
            }
            for (var i = 0; i < noDataBtn2.length; i++){
              noDataBtn2[i].style.display = "";
            }
            for (var i = 0; i < noDataText.length; i++){
              noDataText[i].style.display = "none";
            }
          }
          else{
            for (var i = 0; i < editPart.length; i++){
              editPart[i].style.display = "none";
            }
            for (var i = 0; i < noDataBtn.length; i++){
              noDataBtn[i].style.display = "none";
            }
            for (var i = 0; i < noDataBtn2.length; i++){
              noDataBtn2[i].style.display = "none";
            }
            for (var i = 0; i < noDataText.length; i++){
              noDataText[i].style.display = "";
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
        hideOperation(){
          var display="display:none";
          if(!this.checked){
            display=""
          }
          return display;
        },
        getDom(tagName,name,value){
          var selectDom = [];
          var dom=document.getElementsByTagName(tagName);
          for (var i=0; i<dom.length; i++) {
              if(value===dom[i].getAttribute(name)){
                  selectDom.push(dom[i]);
              }
          }
          return selectDom;
        },
        showEditPCArea(o){
          var parentElement = o.parentElement.parentElement.childNodes;
          var inputEl = parentElement[2];
          var textEl = parentElement[3];
          inputEl.value = textEl.innerText;
          inputEl.style.display = "";
          textEl.style.display = "none";
          inputEl.focus();
        },
        hideEidtPCArea(o){
          o.style.display = "none";
          o.nextElementSibling.style.display = "";
        },
        submitEditPC(o){
          if (event.keyCode == 13){
            var textElement = o.nextElementSibling;
            var tid = textElement.getAttribute("tid");
            var pkid = textElement.getAttribute("pkid");
            var partCode = o.value;
            if(partCode == ""){
              this.$message({message: "零件号不能为空",type: "error"});
              return;
            }
            var submitRequest = {
              id : pkid,
              tid : tid,
              PartCode : partCode
            };
            appSvc.updatePartCode(submitRequest).then(res=>{
              var data = res.data;
              if(data){
                this.$message({message: "修改成功",type: "success"});
                this.getBaoYangPartAdaptations();
                o.blur();
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
        showDeletePCConfirm(o){
          this.$confirm('确认删除零件号？', '提示', {
          confirmButtonText: '确认删除',
          cancelButtonText: '取消',
          closeOnClickModal: false,
          type: 'warning'
          }).then(() => {
            var spanElement = o.parentElement.nextElementSibling.nextElementSibling;
            var tid = spanElement.getAttribute("tid");
            var pkid = spanElement.getAttribute("pkid");
            var deleteRequest = {
              tid:tid,
              id:pkid
            };
            appSvc.deletePartCode(deleteRequest).then(res=>{
                var data = res.data;
                if(data){
                   this.$message({message: "删除成功",type: "success"});
                   this.getBaoYangPartAdaptations();
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
        addPartCode(o){
          var spanElement = o.parentElement.parentElement.childNodes[2];
          this.addPartRequest.tid = spanElement.getAttribute("tid");
          var partName = spanElement.getAttribute("partname");
          this.addPartRequest.partName = partName;
          this.addPartRequest.oeCode = spanElement.innerText;
          this.addPartRequest.partCode = '';
          this.partCodeBrandList = [];
          this.addPartRequest.brand = '';
          for(var i=0;i<this.partNameConfig.length;i++){
            if(this.partNameConfig[i].displayName == partName){
              this.partCodeBrandList = this.partNameConfig[i].brands;
              break;
            }
          }
          this.dialogFormVisible = true;
          this.$nextTick(() => document.getElementById("inputPartCode").focus())
        },
        submitAddPartCode(){
          if(this.addPartRequest.partCode==""){
            this.$message({message: "零件号不能为空",type: "error"});
            return;
          }
          if(this.addPartRequest.brand==""){
            this.$message({message: "品牌不能为空",type: "error"});
            return;
          }
          var partCodeList = new Array();
          var partCodeModel={partCode:this.addPartRequest.partCode,brand:this.addPartRequest.brand};
          partCodeList.push(partCodeModel);
          var submitRequest = {
            tidList:new Array(this.addPartRequest.tid),
            partName:this.addPartRequest.partName,
            oePartCode:this.addPartRequest.oeCode,
            partCodes:partCodeList
          };
          appSvc.insertPartCode(submitRequest).then(res=>{
            this.$message({message: "添加成功",type: "success"});
            this.dialogFormVisible = false;
            this.getBaoYangPartAdaptations();
          },error=>{
              console.log(error)
          })
          .catch(()=>{})
          .finally(()=>{})
        },
        showEditOeArea(o){
          var parentElement = o.parentElement;
          var inputElement = parentElement.previousElementSibling.previousElementSibling;
          var spanElement = parentElement.previousElementSibling;
          inputElement.value = spanElement.innerText;
          spanElement.style.display = "none";
          inputElement.style.display = "";
          inputElement.focus();
        },
        hideEidtOeArea(o){
          o.style.display = "none";
          o.nextElementSibling.style.display = "";
        },
        submitEditOe(o){
          if(event.keyCode == 13){
            var tid = o.nextElementSibling.getAttribute("tid");
            var partName = o.nextElementSibling.getAttribute("partname");
            var originOePartCode = o.nextElementSibling.innerText;
            var  oeCode = o.value;
            var submitRequest = {
              tid:tid,
              partName:partName,
              originalOePartCode:originOePartCode,
              oePartCode:oeCode
            };
            appSvc.updateOePartCode(submitRequest).then(res=>{
              this.$message({message: "修改成功",type: "success"});
              this.getBaoYangPartAdaptations();
              o.blur();
            },error=>{
                console.log(error)
            })
            .catch(()=>{})
            .finally(()=>{})
          }
        },
        showDeleteOeConfirm(o){
          this.$confirm('确认删除oe号及其对应关系？', '提示', {
          confirmButtonText: '确认删除',
          cancelButtonText: '取消',
          closeOnClickModal: false,
          type: 'warning'
          }).then(() => {
            var spanElement = o.parentElement.previousElementSibling;
            var oeCode = spanElement.innerText;
            var tid = spanElement.getAttribute("tid");
            var partName = spanElement.getAttribute("partname");
            var deleteRequest = {
              oePartCode:oeCode,
              tid:tid,
              partName:partName
            };
            appSvc.deletePartCode(deleteRequest).then(res=>{
                var data = res.data;
                if(data){
                   this.$message({message: "删除成功",type: "success"});
                   this.getBaoYangPartAdaptations();
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
        showDeleteSpecialPartConfirm(o){
          var partType = o.parentElement.previousElementSibling.innerText;
          this.$confirm('确认删除'+partType+'及其对应关系？', '提示', {
          confirmButtonText: '确认删除',
          cancelButtonText: '取消',
          closeOnClickModal: false,
          type: 'warning'
          }).then(() => {
            var tid = o.parentElement.parentElement.parentElement.getAttribute("tid");
            var partName = o.parentElement.parentElement.parentElement.parentElement.parentElement.parentElement.parentElement.childNodes[0].innerText;
            var deleteRequest = {
              partType:partType,
              tid:tid,
              partName:partName
            };
            appSvc.deletePartCode(deleteRequest).then(res=>{
                var data = res.data;
                if(data){
                   this.$message({message: "删除成功",type: "success"});
                   this.getBaoYangPartAdaptations();
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
        addSpecialPartCode(o){
          var trElement = o.parentElement.parentElement.parentElement;
          this.addSpecialPartRequest.tid = trElement.getAttribute("tid");
          var partName = trElement.parentElement.parentElement.parentElement.parentElement.childNodes[0].innerText;
          var defaultPartTtype = o.parentElement.previousElementSibling.innerText;
          this.addSpecialPartRequest.partName = partName;
          this.addSpecialPartRequest.partCode = '';
          this.addSpecialPartRequest.brand = '';
          this.addSpecialPartRequest.partType = defaultPartTtype;
          this.partCodeBrandList = [];
          this.partTypeList = [];

          for(var i=0;i<this.partNameConfig.length;i++){
            if(this.partNameConfig[i].displayName == partName){
              this.partCodeBrandList = this.partNameConfig[i].brands;
              this.partTypeList = this.partNameConfig[i].partNames;
              break;
            }
          }
          this.dialogSpecialFormVisible = true;
          this.$nextTick(() => document.getElementById("inputSpecialPartCode").focus())
        },
        submitAddSpecialPartCode(o){
          if(this.addSpecialPartRequest.partCode == ""){
            this.$message({message: "零件号不能为空",type: "error"});
            return;
          }
          if(this.addSpecialPartRequest.partType == ""){
            this.$message({message: "种类不能为空",type: "error"});
            return;
          }
          var partCodeList = new Array();
          var partCodeModel={partCode:this.addSpecialPartRequest.partCode,brand:this.addSpecialPartRequest.brand,partType:this.addSpecialPartRequest.partType};
          partCodeList.push(partCodeModel);
          var submitRequest = {
            TidList:new Array(this.addSpecialPartRequest.tid),
            PartName:this.addSpecialPartRequest.partName,
            partCodes:partCodeList
          };
          appSvc.insertSpecialPartCode(submitRequest).then(res=>{
            this.$message({message: "添加成功",type: "success"});
            this.dialogSpecialFormVisible = false;
            this.getBaoYangPartAdaptations();
          },error=>{
              console.log(error)
          })
          .catch(()=>{})
          .finally(()=>{})
        },
        loadAttributeHtml(){
          var content = "";
          for(var i=0;i<this.partNameConfig.length;i++){
            var partName = this.partNameConfig[i].displayName;
            var isSpecial = this.partNameConfig[i].isSpecialPart;
            var brands = this.partNameConfig[i].brands;
            var partTypes = this.partNameConfig[i].partNames;
            content += "<table isSpecial="+isSpecial+" name='data_"+partName+"' style='display:none;float:left;margin-top:30px;width:100%' class='dialogTable'>";
            content += '<tr><th><label>零件名称:</label></th><td style="padding-left:8px;"><span>'+partName+'</span></td></tr>';
            if(!isSpecial){
              content += '<tr><th><label>OE件号:</label></th><td style="padding-left:8px;"><input name="multaddOE-oeCode" class="form-input"><input type="button" style="margin-left:8px;" class="btn btn-primary" value="确定" onclick="loadOeMapping(this)"></td></tr>'
              content += '<tr name="multPartHtml"><th style="white-space:nowrap;"><label>零件号:</label></th><td style="padding-left:8px;"><input name="multAddOE-pc" class="form-input"><span style="margin-left:10px;">品牌:</span>'
              content += '<select name="multAddOE-brand" class="form-select" style="width: 25%;margin-left:8px;">'
              for(var j=0;j<brands.length;j++){
                content += '<option value="'+brands[j]+'">'+brands[j]+'</option>'
              }
              content += '</select><span class="ui-icon ui-icon-plus" style="display: inline-block; margin-left: 5px; cursor: pointer" onclick="addPCArea(this)"></span><span class="ui-icon ui-icon-minus" style="display: inline-block; margin-left: 5px; cursor: pointer" onclick="removePCArea(this)"></span></td></tr>'
            }
            else{
              content += '<tr name="multSpecialPartCode"><th><label>零件号:</label></th><td style="padding-left:8px;"><input name="specialPartCode" class="form-input">';
              content += '<span style="margin-left:10px;">品牌:</span><select name="specialPartBrand" class="form-select" style="width: 20%;margin-left:8px;">';
              content += '<option value="">无</option>';
              for(var j=0;j<brands.length;j++){
                content += '<option value="'+brands[j]+'">'+brands[j]+'</option>'
              }
              content += '</select><span style="margin-left:8px;">种类:</span><select name="specialPartType" class="form-select" style="width: 20%;margin-left:8px;">';
              for(var j=0;j<partTypes.length;j++){
                content += '<option value="'+partTypes[j]+'">'+partTypes[j]+'</option>'
              }
              content += '</select><span class="ui-icon ui-icon-plus" style="display: inline-block; margin-left: 5px; cursor: pointer;" onclick="addSpecialPCArea(this)"></span><span class="ui-icon ui-icon-minus" style="display: inline-block; margin-left: 5px; cursor: pointer" onclick="removePCArea(this)"></span></td></tr>'
            }
            content += "</table>"
          }
          this.innerHtmlAttribute = content;
        },
        closeDialog(){
          this.innerHtmlAttribute = '';//清空数据
          this.dialogVisibleV1 = false;
        },
        addOEAndMapping(o){
          this.loadAttributeHtml();
          var oldElement = document.getElementsByName("dataPartHtml");
          if(oldElement.length > 0){
            for(var i=0;i<oldElement[0].childNodes.length;i++){
              oldElement[0].childNodes[i].style.display="none";
            }
          }
          var partName = o.getAttribute("partname");
          var tid = o.getAttribute("tid");
          this.dialogTitle = "添加" + partName;
          this.disable = true;
          this.clearable = false;
          this.selectPartList = new Array(partName);
          this.selectTidList = new Array(tid);
          this.dialogVisibleV1 = true;
          this.$nextTick(() => document.getElementsByName("data_"+partName)[0].style.display="")
        },
        buikAddPartCode(){
          this.loadAttributeHtml();
          var oldElement = document.getElementsByName("dataPartHtml");
          if(oldElement.length > 0){
            for(var i=0;i<oldElement[0].childNodes.length;i++){
              oldElement[0].childNodes[i].style.display="none";
            }
          }
          this.dialogTitle = "批量添加";
          this.disable = false;
          this.clearable = false;
          this.selectPartList = [];
          this.selectTidList = [];
          this.dialogVisibleV1 = true;
        },
        changePartCode(){
          for(var i=0;i<this.partNameConfig.length;i++){
            var partName = this.partNameConfig[i].displayName;
            if(this.selectPartList.indexOf(partName)>=0){
              document.getElementsByName("data_"+partName)[0].style.display=""
            }
            else{
              document.getElementsByName("data_"+partName)[0].style.display="none"
            }
          }
        },
        addPCArea(o){
          var parentElement = o.parentElement.parentElement.parentElement;
          var partName = parentElement.childNodes[0].childNodes[1].childNodes[0].innerText;
          var brands = new Array();
          for(var i=0;i<this.partNameConfig.length;i++){
            if(this.partNameConfig[i].displayName == partName){
              brands = this.partNameConfig[i].brands;
              break;
            }
          }
          
          var content = '<tr name="multPartHtml"><th style="white-space:nowrap;"><label>零件号:</label></th><td style="padding-left:8px;"><input name="multAddOE-pc" class="form-input"><span style="margin-left:10px;">品牌:</span>';
          content += '<select name="multAddOE-brand" class="form-select" style="width: 25%;margin-left:8px;">'
          for(var j=0;j<brands.length;j++){
            content += '<option value="'+brands[j]+'">'+brands[j]+'</option>'
          }
          content += '</select><span class="ui-icon ui-icon-plus" style="display: inline-block; margin-left: 5px; cursor: pointer" onclick="addPCArea(this)"></span><span class="ui-icon ui-icon-minus" style="display: inline-block; margin-left: 5px; cursor: pointer" onclick="removePCArea(this)"></span></td></tr>';
          var newnode = document.createElement("tr");
          newnode.innerHTML = content;
          newnode.setAttribute("name","multPartHtml");
          parentElement.appendChild(newnode);
        },
        removePCArea(o){
          var trElement = o.parentElement.parentElement;
          var isSpecial = trElement.parentElement.parentElement.getAttribute("isSpecial");
          var trCount = trElement.parentElement.getElementsByTagName("tr").length;
          var minCount = 3;
          if(isSpecial == "true"){
            minCount = 2;
          }
          if(trCount > minCount){
            trElement.remove();
          }
          else{
            this.$message({message: "最后一条不能移除",type: "warning"});
          }
        },
        addSpecialPCArea(o){
          var parentElement = o.parentElement.parentElement.parentElement;
          var partName = parentElement.childNodes[0].childNodes[1].childNodes[0].innerText;
          var brands = new Array();
          var partTypes = new Array();
          for(var i=0;i<this.partNameConfig.length;i++){
            if(this.partNameConfig[i].displayName == partName){
              brands = this.partNameConfig[i].brands;
              partTypes = this.partNameConfig[i].partNames;
              break;
            }
          }
          var content = '<tr name="multSpecialPartCode"><th><label>零件号:</label></th><td style="padding-left:8px;"><input name="specialPartCode" class="form-input">';
          content += '<span style="margin-left:10px;">品牌:</span><select name="specialPartBrand" class="form-select" style="width: 20%;margin-left:8px;">';
          content += '<option value="">无</option>';
          for(var j=0;j<brands.length;j++){
            content += '<option value="'+brands[j]+'">'+brands[j]+'</option>'
          }
          content += '</select><span style="margin-left:8px;">种类:</span><select name="specialPartType" class="form-select" style="width: 20%;margin-left:8px;">';
          for(var j=0;j<partTypes.length;j++){
            content += '<option value="'+partTypes[j]+'">'+partTypes[j]+'</option>'
          }
          content += '</select><span class="ui-icon ui-icon-plus" style="display: inline-block; margin-left: 5px; cursor: pointer;" onclick="addSpecialPCArea(this)"></span><span class="ui-icon ui-icon-minus" style="display: inline-block; margin-left: 5px; cursor: pointer" onclick="removePCArea(this)"></span></td></tr>'
          var newnode = document.createElement("tr");
          newnode.innerHTML = content;
          newnode.setAttribute("name","multSpecialPartCode");
          parentElement.appendChild(newnode);
        },
        loadOeMapping(o){
          var oeCode = o.previousElementSibling.value;
          var partName = o.parentElement.parentElement.parentElement.childNodes[0].childNodes[1].childNodes[0].innerText;
          if(oeCode == ""){
            this.$message({message: "请输入OE件号",type: "warning"});
            return;
          }
          var queryReuqest = {
            oeCode:oeCode,
            partName:partName
          };
          appSvc.getPartCodeAndBrandByOe(queryReuqest).then(res=>{
            var data = res.data;
            if(data!=null&&data.length>0){
              var removeElement = o.parentElement.parentElement.parentElement;
              var childNodes = removeElement.childNodes;
              for(var x=childNodes.length - 1;x>=2;x--){
                childNodes[x].remove();
              }

              var brands = new Array();
              for(var i=0;i<this.partNameConfig.length;i++){
                if(this.partNameConfig[i].displayName == partName){
                  brands = this.partNameConfig[i].brands;
                  break;
                }
              }
              for(var x=0;x<data.length;x++){
                var content = '<tr name="multPartHtml"><th style="white-space:nowrap;"><label>零件号:</label></th><td style="padding-left:8px;"><input name="multAddOE-pc" class="form-input" value="'+data[x].partCode+'"><span style="margin-left:10px;">品牌:</span>';
                content += '<select name="multAddOE-brand" class="form-select" style="width: 25%;margin-left:8px;">'
                for(var j=0;j<brands.length;j++){
                  if(brands[j] == data[x].brand){
                    content += '<option value="'+brands[j]+'" selected>'+brands[j]+'</option>'
                  }
                  else{
                    content += '<option value="'+brands[j]+'">'+brands[j]+'</option>'
                  }
                }
                content += '</select><span class="ui-icon ui-icon-plus" style="display: inline-block; margin-left: 5px; cursor: pointer" onclick="addPCArea(this)"></span><span class="ui-icon ui-icon-minus" style="display: inline-block; margin-left: 5px; cursor: pointer" onclick="removePCArea(this)"></span></td></tr>';
                var newnode = document.createElement("tr");
                newnode.innerHTML = content;
                newnode.setAttribute("name","multPartHtml");
                removeElement.appendChild(newnode);
              }
            }
            else{
              this.$message({message: "无数据",type: "warning"});
            }
          },error=>{
              console.log(error)
          })
          .catch(()=>{})
          .finally(()=>{})
        },
        addPartCodeCommon(){
          if(this.selectPartList.length == 0){
            this.$message({message: "请选择配件",type: "error"});
            return;
          }
          if(this.selectTidList.length == 0){
            this.$message({message: "选择需要添加的TID",type: "error"});
            return;
          }
          var normalRequest = new Array();
          var specialRequest = new Array();
          for(var i=0;i<this.selectPartList.length;i++){
            var element = document.getElementsByName("data_"+this.selectPartList[i])[0];
            var childElement = element.childNodes[0].childNodes;
            if(element.getAttribute("isspecial") == "false"){
              var subList = new Array();
              for(var x=2;x<childElement.length;x++){
                var inputValue = childElement[x].childNodes[1].childNodes[0].value;
                if(inputValue == ""){
                  this.$message({message: "零件号不能为空",type: "error"});
                  return;
                }
                var subItem = {
                  partCode:inputValue,
                  brand:childElement[x].childNodes[1].childNodes[2].value
                }
                subList.push(subItem);
              }
              var normalItem = {
                partName:this.selectPartList[i],
                oePartCode:childElement[1].childNodes[1].childNodes[0].value,
                partCodes:subList
              };
              normalRequest.push(normalItem);
            }
            else{
              var subList = new Array();
              for(var x=1;x<childElement.length;x++){
                var inputValue = childElement[x].childNodes[1].childNodes[0].value;
                if(inputValue == ""){
                  this.$message({message: "零件号不能为空",type: "error"});
                  return;
                }
                var subItem = {
                  partCode:inputValue,
                  brand:childElement[x].childNodes[1].childNodes[2].value,
                  partType:childElement[x].childNodes[1].childNodes[4].value
                }
                subList.push(subItem);
              }
              var specialItem = {
                partName:this.selectPartList[i],
                partCodes:subList
              };
              specialRequest.push(specialItem);
            }
          }
          var submitRequest = {
            tidList:this.selectTidList,
            normalPart:normalRequest,
            specialPart:specialRequest
          }
          appSvc.addBaoYangPartCommon(submitRequest).then(res=>{
            var data = res.data;
            if(data){
              this.$message({message: "添加成功",type: "success"});
              this.closeDialog();
              this.getBaoYangPartAdaptations();
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
        deleteAllOEs(o){
          var tid = o.getAttribute("tid");
          var partName = o.getAttribute("partname");
          var deleteRequest = {
            tid:tid,
            partName:partName
          };
          appSvc.deletePartCode(deleteRequest).then(res=>{
            var data = res.data;
            if(data){
                this.$message({message: "删除成功",type: "success"});
                this.getBaoYangPartAdaptations();
            }
            else{
              this.$message({message: "删除失败",type: "error"});
            }
          },error=>{
              console.log(error)
          })
          .catch(()=>{})
          .finally(()=>{})
        },
        selectAll(){
          var allTid = new Array();
          for(var d=0;d<this.partAdaptationList.length;d++){
            allTid.push(this.partAdaptationList[d].tid);
          }
          if(this.tidChecked){
            this.selectTidList = allTid;
          }
          else{
            this.selectTidList = [];
          }
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

/deep/ .tableContainer th {
    height: 32px;
    font-size: 75%;
    background: #66ABEF;
    color: #F3F7FA;
    border: 1px solid #559BD0;
    font-weight: 600;
    font-family: "Microsoft Yahei";
    text-align: center;
    min-width: 250px;
}

.table-responsive {
  overflow-x: auto;
}

/deep/ .tableContainer td {
    font-size: 75%;
    padding: 8px 3px 8px 3px;
    font-family: "Microsoft Yahei";
    text-align: center;
    border: 1px solid #eee;
    min-width: 250px;
}

/deep/ .clearPadding {
    padding: 0 !important;
    vertical-align: top;
    border: 2px solid black !important;
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

/deep/ table{
    border-collapse: collapse;
    clear: both;
    border: 0;
}

/deep/ .operation-group {
    list-style: none;
    padding: 0;
    margin: 0;
    float: right;
}

/deep/ .operation-group li {
    float: right;
    cursor: pointer;
    margin-left: 5px;
    padding: 3px;
}

/deep/ .operation-group .ui-state-error {
    border: 0;
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

/deep/ .ui-icon-plus{
  background-position:-16px -128px;
}

/deep/ .ui-icon-minus{
  background-position:-48px -128px;
}

/deep/ .ui-icon-trash{
  background-position:-176px -96px;
}

/deep/ .operation-Add {
    list-style: none;
    padding-left: 0;
    margin: 0;
}

/deep/ .operation-Add li {
    text-align: center;
    padding: 3px 8px 3px 8px;
    border-top: 1px solid;
    border-bottom: 1px solid;
    cursor: pointer;
    background-color: #F7F7F7;
}

/deep/ .operation-Add li:hover, .operation-hover:hover {
    background-color: #fffccc;
}

/deep/ .operation-Add li:nth-last-child(1) {
    border-right: 0;
}

/deep/ .bage {
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
  border-radius: 5px;
}

/deep/ .bage-primary {
  background-color: #337ab7;
}

/deep/ .bage-nodata {
  background-color: black;
}

/deep/ .bage-notavailable {
  background-color: red;
}

/deep/ .bage-noproduct {
  background-color: #3c763d;
}
/deep/ .bage-tooltip {
  background-color: #ffffff;
  border: 1px solid #3079ED;
}

/deep/ .bage-tooltipnotavailable {
  background-color: #ffffff;
  border: 1px solid red;
}
/deep/ .bage-tooltipnotavailable a {
  color: red !important;
}

/deep/ .tooltip {
  height: auto;
  text-align: center;
  position: absolute;
  margin-left: 10px;
  padding: 8px;
  margin-top: -7px;
  z-index: 99;
}

/deep/ .oeCode {
    color: #08c;
    font-weight: bolder;
}

/deep/ a{
  color: #08c
}

/deep/ .el-dialog__body{
  padding: 20px 20px !important;
  overflow:auto !important;
  max-height: 530px !important;
}

/deep/ .form-input{
  width: 120px;
  line-height: 1.42857143;
  height: 30px;
  padding: 2px 2px 2px 5px;
  color: #555;
  background-color: #fff;
  background-image: none;
  border: 1px solid #ccc;
  border-radius: 4px;
  -webkit-box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
  box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
  -webkit-transition: border-color ease-in-out .15s,-webkit-box-shadow ease-in-out .15s;
  -o-transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
  transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
}

/deep/ .form-select{
  line-height: 1.42857143;
  height: 30px;
  padding: 2px 2px 2px 5px;
  color: #555;
  background-color: #fff;
  background-image: none;
  border: 1px solid #ccc;
  border-radius: 4px;
  -webkit-box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
  box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
  -webkit-transition: border-color ease-in-out .15s,-webkit-box-shadow ease-in-out .15s;
  -o-transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
  transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
}

/deep/ .btn {
  height: 30px;
  display: inline-block;
  padding: 6px 12px;
  margin-bottom: 0;
  font-size: 13px;
  font-weight: normal;
  line-height: 1.42857143;
  text-align: center;
  white-space: nowrap;
  vertical-align: middle;
  -ms-touch-action: manipulation;
      touch-action: manipulation;
  cursor: pointer;
  -webkit-user-select: none;
     -moz-user-select: none;
      -ms-user-select: none;
          user-select: none;
  background-image: none;
  border: 1px solid transparent;
  border-radius: 4px;
}

/deep/ .btn-primary{
  color: #fff;
  background-color: #5cb85c;
  border-color: #4cae4c;
}

/deep/ .btn-primary:hover{
  color: #fff;
  background-color: #449d44;
  border-color: #398439;
}

.form {
  label{
    width: 80px;
    font-weight: bolder;
    font-size: 1.05em;
    display: inline-block;
    height: 30px;
    max-width: 100%;
    margin-bottom: 5px;
    font-weight: bold;
    margin-left: 10px;
  }
  div{
    margin: 5px 5px 5px 5px;
  }
  span{
    margin-left: 9px;
  }
}
/deep/ .dialogTable{
  th{
    width: 85px;
    height: 36px;
    background-color:#E8EEF4;
    border: 1px solid #FFFFFF;
  }
  td{
    border: 1px solid #eee;
  }
}

.abow_dialog {
    display: flex;
    justify-content: center;
    align-items: Center;
    overflow: hidden;
    margin-top: -15vh;
    .el-dialog {
        margin: 0 auto !important;
        height: 90%;
        overflow: hidden;
        .el-dialog__body {
            position: absolute;
            left: 0;
            top: 54px;
            bottom: 0;
            right: 0;
            padding: 0;
            z-index: 1;
            overflow: hidden;
            overflow-y: auto;
        }
    }
}
</style>