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
  <el-button type="success" size="small" @click="getBaoYangPropertyAdaptation()" >查询</el-button>
</el-row>
</div>
<div class="box">
    <el-select v-model="partType" filterable placeholder="选择需要显示的配件" style="margin-top:10px;width:500px;" size="small">
        <el-option
        v-for="item in partTypeList"
        :key="item.baoYangType"
        :label="item.displayName"
        :value="item.baoYangType"></el-option>
    </el-select>
</div>
<el-table v-loading="tableLoading" :data="propertyAdaptationList" border style="width: 100%;margin-top:20px;">
    <el-table-column label="TID" prop="tid" width="80"></el-table-column>
    <el-table-column label="VehicleID" prop="vehicleId" width="150"></el-table-column>
    <el-table-column label="品牌" prop="brand" width="100"></el-table-column>
    <el-table-column label="车系" prop="vehicle" width="150"></el-table-column>
    <el-table-column label="排量" prop="paiLiang" width="80"></el-table-column>
    <el-table-column label="发动机型号" prop="engineNo" width="120"></el-table-column>
    <el-table-column label="生产年份" prop="packagePrice" width="180">
        <template slot-scope="scope">					
            {{scope.row.listedYear}}-{{scope.row.stopProductionYear}}					
        </template>
    </el-table-column>
    <el-table-column label="年款" prop="nian" width="80"></el-table-column>
    <el-table-column label="版型" prop="salesName" width="200"></el-table-column>
    <el-table-column label="燃油类型" prop="fuelType" width="100"></el-table-column>
    <el-table-column label="OE件号" prop="oeCode" width="100"></el-table-column>
    <el-table-column label="零件号" prop="partCode" width="100"></el-table-column>
    <el-table-column fixed="right" label="属性" width="100">
        <template slot-scope="scope">
            <span v-show="!scope.row.isEdit">{{scope.row.property}}</span>
            <el-select size="mini" v-model="scope.row.property" v-show="scope.row.isEdit" filterable>
                <el-option v-for="item in propertyDescription" :key="item.displayName" :label="item.displayName" :value="item.displayName"></el-option>
            </el-select>
        </template>
    </el-table-column>
    <el-table-column fixed="right" label="属性值" width="100">
        <template slot-scope="scope">
            <span v-show="!scope.row.isEdit">{{scope.row.propertyValue}}</span>
            <el-input size="mini" v-show="scope.row.isEdit" v-model="scope.row.propertyValue"></el-input>
        </template>
    </el-table-column>
    <el-table-column fixed="right" label="图片链接" width="100">
        <template slot-scope="scope">
            <el-image v-show="scope.row.imageUrl != ''" @click="routeImageDetail(scope.row.imageUrl)" style="width: 25px; height: 25px" :src="scope.row.imageUrl"></el-image>
            <el-upload style="float:right;" v-show="scope.row.isEdit" action="http://upload.qiniup.com/" :before-upload="beforeAvatarUpload" :show-file-list="false"
                   accept="image/jpeg, image/png, image/jpg" :http-request="uploadRequest" :data="{pkId:scope.$index}">
                <i class="el-icon-upload"></i>
            </el-upload>
        </template>
    </el-table-column>
    <el-table-column fixed="right" align="center" label="操作">
        <template slot-scope="scope">
            <el-button type="primary" v-show="!scope.row.isEdit" icon="el-icon-edit" size="mini" @click="showEdit(scope.$index)"></el-button>
            <el-button type="success" v-show="scope.row.isEdit" style="margin-left:0px" icon="el-icon-folder-checked" size="mini" @click="editData(scope.$index)"></el-button>
            <el-button type="danger" v-show="scope.row.id > 0" icon="el-icon-delete" size="mini" @click="deleteOeCode(scope.row.id)"></el-button>
        </template>
    </el-table-column>
</el-table>
</main>
</template>

<script>
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";
import { appSvc } from "@/api/baoyang/baoyang";
import { upload } from '@/utils/uploadfile'
export default {
    name:'propertyadaptation',
        data() {
      return {
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
        partTypeList:[],
        partType:'',
        tableLoading:false,
        propertyAdaptationList:[],
        propertyDescription:[]
      }
    },
    created(){
        this.getBrandList();
        this.getBaoYangParts();
        this.getBaoYangProperts();
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
        getBaoYangParts(){
            appSvc.getBaoYangParts().then(
            res => {
                var data = res.data;
                this.partTypeList = data;
            },
            error => {
                console.log(error)
            }
            )
            .catch(() => {})
            .finally(() => {});
        },
        getBaoYangPropertyAdaptation(){
            var tabType = this.activeName;
            var propertyRequest={
                vehicleId:'',
                paiLiang:'',
                nian:'',
                tid:'',
                baoYangType:this.partType};
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
                propertyRequest.vehicleId=this.vehicleId;
                propertyRequest.paiLiang=this.paiLiang;
                propertyRequest.nian=this.nian;
                propertyRequest.tid=this.tid;
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
                propertyRequest.vehicleId=this.secondVehicleId;
                propertyRequest.paiLiang=this.secondPaiLiang;
            }
            else if(tabType=="3"){
                if(this.secondTid==''){
                    this.$message({message: "请输入Tid",type: "error"});
                    return;
                }
                propertyRequest.tid=this.secondTid;
            }
            else{
                return;
            }
            if(this.partType==''){
                this.$message({message: "请选择要显示的配件",type: "error"});
                return;
            }
            this.tableLoading = true;
            console.log("condition: " + JSON.stringify(propertyRequest));
            appSvc.getBaoYangPropertyAdaptation(propertyRequest).then(res=>{
                var data = res.data;
                this.propertyAdaptationList=data;
            },error=>{
                console.log(error)
            })
            .catch(()=>{})
            .finally(()=>{
                this.tableLoading = false;
            })
        },
        showEdit(index){
            this.propertyAdaptationList[index].isEdit = true;
        },
        editData(index){
            var rowObj = this.propertyAdaptationList[index];
            var id = rowObj.id;
            var tid = rowObj.tid;
            var oeCode = rowObj.oeCode;
            var partCode = rowObj.partCode;
            var property = rowObj.property;
            var propertyValue = rowObj.propertyValue;
            var imageUrl = rowObj.imageUrl;
            var baoYangType = rowObj.baoYangType;
            if(property == ''){
                this.$message({message: "请选择属性",type: "warning"});
                return;
            }
            if(propertyValue == ''){
                this.$message({message: "请输入属性值",type: "warning"});
                return;
            }
            var saveRequest = {
                id:id,
                tid:tid,
                baoYangType:baoYangType,
                property:property,
                propertyValue:propertyValue,
                oePartCode:oeCode,
                partCode:partCode,
                imageUrl:imageUrl
            };
            appSvc.savePropertyAdaptation(saveRequest).then(res=>{
                var data = res.data;
                if(data){
                    this.$message({message: "保存成功",type: "success"});
                    this.getBaoYangPropertyAdaptation();
                }
                else{
                    this.$message({message: "添加失败",type: "error"});
                }
            },error=>{
                console.log(error)
            })
            .catch(()=>{})
            .finally(()=>{})
        },
        deleteOeCode(oeCodeId){
            this.$confirm('确认要删除吗？', '提示', {
            confirmButtonText: '确认',
            cancelButtonText: '取消',
            closeOnClickModal: false,
            type: 'warning'
            }).then(() => {
                var deleteRequet = {
                    id:oeCodeId
                };
                appSvc.deletePropertyAdaptation(deleteRequet).then(res=>{
                    var data = res.data;
                    if(data){
                        this.$message({message: "删除成功",type: "success"});
                        this.getBaoYangPropertyAdaptation();
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
        routeImageDetail(url){
            window.open(url);
        },
        getBaoYangProperts(){
            appSvc.getBaoYangPropertyDescription().then(
            res => {
                var data = res.data;
                this.propertyDescription = data;
            },
            error => {
                console.log(error)
            }
            )
            .catch(() => {})
            .finally(() => {});
        },
        beforeAvatarUpload(file) {
            const isLt2M = file.size / 1024 / 1024 < 5;
            if (!isLt2M) {
                this.$message.error("上传图片大小不能超过 5MB!");
            }
            return isLt2M;
        },
        uploadRequest: function(request) {
            var index = request.data.pkId;
            const loading = this.$loading({
                lock: true,
                text: '拼命上传中......',
                spinner: 'el-icon-loading',
                background: 'rgba(0, 0, 0, 0.7)'
            });

            var fileName = request.file.name;
            var fileExtension = fileName.split(".").pop();
            const newFileName = appSvc.formatDate(new Date(), "yyyyMMddhhmmss") + appSvc.getRandomInt(1000, 9999) + "." + fileExtension;
            var directoryName = 'BaoYang/Images'
            var key = directoryName + "/" + newFileName;
            this.getToken(directoryName, fileName).then(res => {
                var result = res.data;
                const token = result.token;
                const host = result.host;
                upload(
                token,
                key,
                request,
                next => {
                    const total = next.total;
                },
                error => {
                    this.fileList = [];
                },
                complete => {
                    const hash = complete.hash;
                    const key = complete.key;
                    var url = host + "/" + key;
                    this.propertyAdaptationList[index].imageUrl = url;
                }
                );
            }).catch(() => { })
            .finally(() => {
                loading.close();
            })
        },
        //获取token
        getToken(directoryName, fileName) {
            return appSvc.getQiNiuToken({
                directory: directoryName,
                fileName: fileName
            })
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
table{
    border-collapse: collapse;
    clear: both;
    border: 0;
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

.innerTable td {
    font-size: 75%;
    text-align: left;
        font-family: "Microsoft Yahei";
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
.operation-group {
    list-style: none;
    padding: 0;
    margin-bottom: 0;
    float: right;
}
.operation-group {
    list-style: none;
    padding: 0;
    margin-bottom: 0;
    float: right;
}
</style>