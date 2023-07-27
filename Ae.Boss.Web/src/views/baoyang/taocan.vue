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
                <el-tab-pane label="输入套餐ID" name="4">
                    <span class="input-label"> 输入套餐ID：</span>
                    <el-input class="input" clearable size="small" placeholder="套餐ID" v-model="packageId"></el-input>
                    <span style="color:red;font-size:12px;margin-left:3px;">(*必选)</span>
                    <span class="input-label"> 输入TID：：</span>
                    <el-input class="input" clearable size="small" placeholder="TID" v-model="thirdTid"></el-input>
                </el-tab-pane>
                </el-tabs>
        </div>

        <div class="buttons">
            <el-row>
                <el-button type="success" size="small" @click="getTaoCanList();">查询</el-button>
                <el-button type="primary" size="small" @click="dialogVisible = true">添加套餐</el-button>
                <!--<el-button type="danger" size="small">删除套餐</el-button>-->
            </el-row>
        </div>

        <el-table v-loading="tableLoading" :data="baoYangPackageRefList" border style="width: 100%;margin-top:20px;">
            <el-table-column label="车型ID" prop="tid" width="100"></el-table-column>
            <el-table-column label="车型名称" prop="vehicleName" width="500"></el-table-column>
            <el-table-column label="套餐ID" prop="packagePid" width="150"></el-table-column>
            <el-table-column label="套餐名称" prop="packageName" width="300"></el-table-column>
            <el-table-column label="套餐类型" prop="byTypeName" width="180"></el-table-column>
            <el-table-column label="套餐价格" prop="packagePrice" width="100"></el-table-column>
            <el-table-column fixed="right" label="操作">
                <template slot-scope="scope">
                    <el-button @click="deletePackagge(scope.$index, baoYangPackageRefList,scope.row.id)" type="text" size="small">删除</el-button>
                </template>
            </el-table-column>
        </el-table>

        <el-dialog title="添加套餐" :close-on-click-modal='false' :visible.sync="dialogVisible" width="50%">
            <span class="input-title"> TID：</span>
            <el-select style="width:calc(100% - 60px);margin-left:5px;" @change="getPackageByTid();" v-model="addTid" filterable placeholder="-请选择车型-" size="small">
                <el-option
                v-for="item in tidList"
                :key="item.tid"
                :label="item.vehicleName"
                :value="item.tid">
                </el-option>
            </el-select>
            <br/> <br/>
            <span class="input-title"> 类型：</span>
            <el-select style="width:calc(100% - 60px);" @change="getPackageByTid();" v-model="baoYangType" filterable placeholder="-选择套餐类型-" size="small">
                <el-option
                v-for="item in baoYangTypeList"
                :key="item.baoYangType"
                :label="item.displayName"
                :value="item.baoYangType">
                </el-option>
            </el-select>
            <br/> <br/>
            <span class="input-title"> 套餐：</span>
            <el-select style="width:calc(100% - 60px);" v-model="packageIdList" multiple filterable placeholder="-选择需要添加的套餐-" size="small">
                <el-option
                v-for="item in packageList"
                :key="item.packagePid"
                :label="item.packageName"
                :value="item.packagePid">
                </el-option>
            </el-select>
            <span slot="footer" class="dialog-footer">
                <el-button type="primary" @click="addBaoYangPackage();">添 加</el-button>
                <el-button @click="dialogVisible = false">取 消</el-button>
            </span>
        </el-dialog>
    </main>
</template>

<script>
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";
import { appSvc } from "@/api/baoyang/baoyang";
export default {
    name:'taocan',
    data(){
        return{
            dialogVisible: false,
            tableLoading: false,
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
            packageId:'',
            thirdTid:'',
            activeName: '1',
            baoYangPackageRefList:[],
            packageIdList:[],
            packageList:[],
            addTid:'',
            tidList:[],
            baoYangTypeList:[],
            baoYangType:''
        }
    },
    created(){
        this.getBrandList();
        this.getPackageByType();
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
        getTaoCanList(){
            var tabType = this.activeName;
            var taoCanRequest={searchType:tabType,
                vehicleId:'',
                paiLiang:'',
                nian:'',
                tid:'',
                packageId:''};
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
                taoCanRequest.vehicleId=this.vehicleId;
                taoCanRequest.paiLiang=this.paiLiang;
                taoCanRequest.nian=this.nian;
                taoCanRequest.tid=this.tid;
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
                taoCanRequest.vehicleId=this.secondVehicleId;
                taoCanRequest.paiLiang=this.secondPaiLiang;
            }
            else if(tabType=="3"){
                if(this.secondTid==''){
                    this.$message({message: "请输入Tid",type: "error"});
                    return;
                }
                taoCanRequest.tid=this.secondTid;
            }
            else if(tabType=="4"){
                if(this.packageId==''){
                    this.$message({message: "请输入套餐ID",type: "error"});
                    return;
                }
                taoCanRequest.packageId=this.packageId;
                taoCanRequest.tid=this.thirdTid;
            }
            else{
                return;
            }
            this.addTid='';
            this.tidList=[];
            this.tableLoading = true;
            console.log("condition: " + JSON.stringify(taoCanRequest));
            appSvc.getBaoYangPackageRef(taoCanRequest).then(res=>{
                var data = res.data;
                this.baoYangPackageRefList = data.packages;
                this.tidList = data.vehicles;

            },error=>{
                console.log(error)
            })
            .catch(()=>{})
            .finally(()=>{
                this.tableLoading = false;
            })
        },
        getPackageByTid(){
            console.log(this.addTid+'--'+this.baoYangType);
            if(this.addTid==''||this.baoYangType==''){
                return;
            }
            this.packageIdList=[];
            this.packageList=[];
            var adaptivePakcage={tid:this.addTid, baoYangType:this.baoYangType};
            appSvc.getBaoYangPackageByTid(adaptivePakcage).then(res=>{
                var data = res.data;
                this.packageList = data;
                if(this.packageList.length==0){
                    this.$message({message: "暂无合适套餐",type: "warning"});
                }
            },error=>{
                console.log(error)
            })
            .catch(()=>{})
            .finally(()=>{})
        },
        getPackageByType(){
            appSvc.getPackageByType().then(res=>{
                var data = res.data;
                this.baoYangTypeList = data;
            },error=>{
                console.log(error)
            })
            .catch(()=>{})
            .finally(()=>{})
        },
        addBaoYangPackage(){
            if(this.packageIdList.length==0){
                this.$message({message: "请选择要添加的套餐",type: "warning"});
                return;
            }
            var addPackageRequest={tid:this.addTid, byType:this.baoYangType,packageId:this.packageIdList};
            appSvc.addBaoYangPackage(addPackageRequest).then(res=>{
                var data = res.data;
                this.$message({message: "添加成功",type: "success"});
                this.dialogVisible = false
                this.getTaoCanList();
            },error=>{
                console.log(error)
            })
            .catch(()=>{})
            .finally(()=>{})
        },
        deletePackagge(index, rows,pkid){
            var deletePackageRequest={id:pkid};
            appSvc.deleteBaoYangPackageRef(deletePackageRequest).then(res=>{
                var data = res.data;
                this.$message({message: "删除成功",type: "success"});
                rows.splice(index, 1);
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
.container {
  margin-left: 30px;
.input-label{
    margin-left:4px;
    font-size:14px;
    }
  }

.input-title{
    font-size: 14px;
    font-weight: bold
}
.buttons{
    margin-top:20px;
}
.input{
    width: 200px;
}
</style>