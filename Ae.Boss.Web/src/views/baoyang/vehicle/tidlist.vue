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
    <el-select v-model="vehicleId" @clear="tid='';salesNameList=[];nian='';nianList=[];paiLiang='';paiLiangList=[];" @change="getPaiLiangByVehicleId()" clearable filterable placeholder="-请选择车系-" size="small">
        <el-option
        v-for="item in vehicleList"
        :key="item.vehicleId"
        :label="item.vehicle"
        :value="item.vehicleId">
        </el-option>
    </el-select>
    <span class="input-label"> 排量：</span>
    <el-select v-model="paiLiang" @clear="tid='';salesNameList=[];nian='';nianList=[];" @change="getVehicleNianByPaiLiang()" clearable filterable placeholder="-请选择排量-" size="small">
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
        <el-button type="success" size="small" @click="getVehicleData();">查询</el-button>
    </el-row>
</div>
<el-input style="margin-top:10px;" type="textarea" :rows="5" placeholder="五级车型Tid" v-model="tidssss" :readonly="true"></el-input>
</main>
</template>

<script>
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";
import { appSvc } from "@/api/baoyang/baoyang";
export default {
    name:'ModelsManage',
    data(){
        return{
            tidssss:'',
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
            innerHtmlData:'',
            vehicleDataList:[]
        }
    },
    created(){
        this.getBrandList();
    },
    methods:{
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
        getVehicleData(){
            debugger;
            var tabType = this.activeName;
            var vehicleRequest={
                brand:'',
                vehicleId:'',
                paiLiang:'',
                nian:'',
                tid:''};
            if(tabType=="1"){
                if(this.brand==''){
                    this.$message({message: "请选择品牌",type: "error"});
                    return;
                }
                vehicleRequest.brand = this.brand;
                vehicleRequest.vehicleId=this.vehicleId;
                vehicleRequest.paiLiang=this.paiLiang;
                vehicleRequest.nian=this.nian;
                vehicleRequest.tid=this.tid;
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
                vehicleRequest.vehicleId=this.secondVehicleId;
                vehicleRequest.paiLiang=this.secondPaiLiang;
            }
            else if(tabType=="3"){
                if(this.secondTid==''){
                    this.$message({message: "请输入Tid",type: "error"});
                    return;
                }
                vehicleRequest.tid=this.secondTid;
            }
            else{
                return;
            }
            this.tidssss = '';
            console.log("condition: " + JSON.stringify(vehicleRequest));
            appSvc.getTidList(vehicleRequest).then(res=>{
                var data = res.data;
                if(data!=null && data.length>0){
                    this.tidssss = data.join(',');
                }
            },error=>{
                console.log(error)
            })
            .catch(()=>{})
            .finally(()=>{})
        },
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

.tableContainer {
    width: auto;
}

.table-responsive {
  overflow-x: auto;
}

/deep/ table{
    border-collapse: collapse;
    clear: both;
    border: 0;
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
  background-image: url("../../../assets/bao_yang_images/ui-icons_469bdd_256x240.png");
  display: block;
}
/deep/ .ui-state-error .ui-icon{
  background-image: url("../../../assets/bao_yang_images/ui-icons_cd0a0a_256x240.png");
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

</style>