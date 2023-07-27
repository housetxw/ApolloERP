<template>
    <main class="container">
        <rg-page id="indexPage" 
            :pageIndex="condition.pageIndex"
            :pageSize="condition.pageSize"
            :dataCount="totalList"
            :tableLoading="tableLoading"
            :tableData="tableData"
            :pageChange="pageChange"
            :headerBtnWidth="180"
            :searching="search">

            <template v-slot:condition>
                <el-form-item>
                    <el-input class="input"
                        clearable
                        placeholder="请输入OE号"
                        style="width:150px"
                        prefix-icon="el-icon-search"
                        v-model="condition.oeCode">
                    </el-input>
                </el-form-item>
                <el-form-item>
                    <el-input class="input"
                        clearable
                        placeholder="请输入配件号"
                        style="width:150px"
                        prefix-icon="el-icon-search"
                        v-model="condition.partCode">
                    </el-input>
                </el-form-item>
                <el-form-item>
                    <el-select v-model="condition.partType" clearable filterable style="width:150px;">
                        <el-option
                        v-for="item in partTypeEnum"
                        :key="item.displayName"
                        :label="item.displayName"
                        :value="item.displayName">
                        </el-option>
                    </el-select>
                </el-form-item>
            </template>

            <template v-slot:header_btn>
                <el-button type="primary" size="mini" icon="el-icon-plus" @click="add">添加OE号</el-button>
            </template>

            <template v-slot:tb_cols>
                <el-table-column label="配件类型" prop="partName"></el-table-column>
                <el-table-column label="OE号" prop="oePartCode"></el-table-column>
                <el-table-column label="配件号" prop="partCode"></el-table-column>
                <el-table-column label="车型品牌" prop="vehicleBrand"></el-table-column>
                <el-table-column label="操作" width="80px">
                    <template slot-scope="scope">
                        <el-button-group>
                        <el-button @click="routeDetail(scope.row.oePartCode)" type="primary" size="mini">查看详情</el-button>
                        </el-button-group>
                    </template>
                </el-table-column>
            </template>
            
        </rg-page>

        <rg-dialog :title="formTitle"
            :visible.sync="dialogVisible"
            :beforeClose="cancel"
            :loading="formLoading"
            :btnCancel="{label:'关闭',click:(done)=>{cancel()}}"
            :btnRemove="false"
            :footbar="true"
            width="80%"
            maxWidth="600px"
            minWidth="600px">
            <template v-slot:content>
                <el-form :model="oeDetail" size="mini" label-width="120px">
                    <el-form-item label="OE号">
                        <el-input readonly style="width:353px;" v-model="oeDetail.oePartCode"></el-input>
                    </el-form-item>
                    <el-form-item label="配件类型">
                        <el-select v-model="oeDetail.partType" @change="changePartType();" filterable style="width:353px;">
                            <el-option
                                v-for="item in partTypeEnum"
                                :key="item.displayName"
                                :label="item.displayName"
                                :value="item.displayName">
                            </el-option>
                        </el-select>
                    </el-form-item>
                    <el-form-item label="车辆品牌">
                        <el-select v-model="oeDetail.vehicleBrand" style="width:353px;" multiple filterable placeholder="-请选择车辆品牌-" size="small">
                            <el-option
                                v-for="item in brandList"
                                :key="item.brand"
                                :label="item.brand"
                                :value="item.brand">
                            </el-option>
                        </el-select>
                    </el-form-item>
                    <el-form-item label="配件">
                        <el-row v-for="(itemPart,index) in oeDetail.partCodes" :key="index">
                            <span style="font-size:12px;">品牌:</span>
                            <el-select style="width:150px" v-model="itemPart.brand" filterable clearable>
                                <el-option
                                    v-for="item in partBrands"
                                    :key="item"
                                    :label="item"
                                    :value="item">
                                </el-option>
                            </el-select>
                            <span style="font-size:12px;margin-left:5px;">配件号:</span>
                            <el-input v-model="itemPart.partCode" style="width:120px"></el-input>
                            <el-button type="danger" icon="el-icon-delete" @click="removePartCode(index)"></el-button>
                        </el-row>
                    </el-form-item>
                </el-form>
            </template>

            <template v-slot:footer>
                <el-button icon="el-icon-delete" size="mini" type="danger" :loading="btnDeleteLoading" @click="deleteOePart()">删除OE号</el-button>
                <el-button
                    icon="el-icon-check"
                    size="mini"
                    type="primary"
                    :loading="btnSaveLoading"
                    @click="editOePart()"
                >提交修改</el-button>
                <el-button icon="el-icon-plus" size="mini" type="success" @click="addPartCode()">添加配件</el-button>
            </template>
        </rg-dialog>

        <rg-dialog :title="formTitle1"
            :visible.sync="dialogVisible1"
            :beforeClose="cancel1"
            :btnCancel="{label:'关闭',click:(done)=>{cancel1()}}"
            :btnRemove="false"
            :footbar="true"
            width="80%"
            maxWidth="600px"
            minWidth="600px">
            <template v-slot:content>
                <el-form :model="addOePartCode" size="mini" label-width="120px">
                    <el-form-item label="OE号">
                        <el-input style="width:353px;" v-model="addOePartCode.oePartCode"></el-input>
                    </el-form-item>
                    <el-form-item label="配件类型">
                        <el-select v-model="addOePartCode.partName" @change="changePartType1();" filterable style="width:353px;">
                            <el-option
                                v-for="item in partTypeEnum"
                                :key="item.displayName"
                                :label="item.displayName"
                                :value="item.displayName">
                            </el-option>
                        </el-select>
                    </el-form-item>
                    <el-form-item label="车辆品牌">
                        <el-select v-model="addOePartCode.vehicleBrand" style="width:353px;" multiple filterable placeholder="-请选择车辆品牌-" size="small">
                            <el-option
                                v-for="item in brandList"
                                :key="item.brand"
                                :label="item.brand"
                                :value="item.brand">
                            </el-option>
                        </el-select>
                    </el-form-item>
                    <el-form-item label="配件">
                        <el-row v-for="(itemPart,index) in addOePartCode.partCode" :key="index">
                            <span style="font-size:12px;">品牌</span>
                            <el-select style="width:150px" v-model="itemPart.brand" filterable clearable>
                                <el-option
                                    v-for="item in partBrands"
                                    :key="item"
                                    :label="item"
                                    :value="item">
                                </el-option>
                            </el-select>
                            <span style="font-size:12px;margin-left:5px;">配件号</span>
                            <el-input v-model="itemPart.partCode" style="width:120px"></el-input>
                            <el-button type="danger" icon="el-icon-delete" @click="removePartCode1(index)"></el-button>
                        </el-row>
                    </el-form-item>
                </el-form>
            </template>

            <template v-slot:footer>
                <el-button
                    icon="el-icon-check"
                    size="mini"
                    type="primary"
                    :loading="btnSaveLoadingA"
                    @click="addOePart()"
                >提交</el-button>
                <el-button icon="el-icon-plus" size="mini" type="success" @click="addPartCode1()">添加配件</el-button>
            </template>
        </rg-dialog>
    </main>
</template>
<script>
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";
import { appSvc } from "@/api/baoyang/baoyang";
export default {
    name:'maintainoemapping',
    data(){
        return{
            tableLoading: false,
            currentPage: 1,
            totalList: 0,
            condition:{
                pageIndex: 1,
                pageSize: 20,
                oeCode:'',
                partCode:'',
                partType:''
            },
            tableData: [],
            partTypeEnum:[],
            brandList:[],
            formTitle:'OE号详情',
            dialogVisible:false,
            formLoading:false,
            oeDetail:{
                oePartCode:'',
                partType:'',
                vehicleBrand:[],
                partCodes:[]
            },
            partBrands:[],
            btnDeleteLoading:false,
            btnSaveLoading:false,
            addOePartCode:{
                oePartCode:'',
                partName:'',
                vehicleBrand:[],
                partCode:[
                    {
                        brand:'',
                        partCode:''
                    }
                ]
            },
            formTitle1:'添加OE号',
            dialogVisible1:false,
            btnSaveLoadingA:false
        }
    },
    created() {
        this.fetchData();
        this.getPartNameList();
        this.getBrandList();
    },
    methods:{
        fetchData() {
            this.getPaged();
        },
        search(flag) {
            this.condition.pageIndex = this.currentPage = 1;
            this.getPaged(flag);
        },
        pageChange(p) {
            this.condition.pageIndex = p.pageIndex;
            this.condition.pageSize = p.pageSize;
            this.getPaged();
        },
        getPaged(flag) {
            this.tableLoading = true;
            appSvc
            .getOeCodeMapList(this.condition)
            .then(
                res => {
                    var data = res.data;
                    this.tableData = data.items;
                    this.totalList = data.totalItems;
                    if (flag) {
                    this.$message({
                        message: res.message || "查询成功",
                        type: "success"
                    });
                    }
                },
                error => {
                    console.log(error);
                }
            )
            .catch(() => {})
            .finally(() => {
                this.tableLoading = false;
            });
        },
        add(){
            this.addOePartCode = {
                oePartCode:'',
                partName:'',
                vehicleBrand:[],
                partCode:[
                    {
                        brand:'',
                        partCode:''
                    }
                ]
            };
            this.dialogVisible1 = true;
        },
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
        getPartNameList(){
          appSvc.getPartNameList().then(
            res => {
                var data = res.data;
                for(var i=0;i<data.length;i++){
                    if(!data[i].isSpecialPart){
                        this.partTypeEnum.push(data[i]);
                    }
                }
            },
            error => {
                console.log(error)
            }
            )
            .catch(() => {})
            .finally(() => {});
        },
        routeDetail(oeCode){
            var detailRequest = {
                oePartCode:oeCode
            };
            this.dialogVisible = true;
            this.formLoading = true;
            appSvc.getOeCodeMapDetailByOeCode(detailRequest)
            .then(res=>{
                var data = res.data;
                this.oeDetail = data;
                this.iniPartBrand(data.partType);
            },
            error=>{
                console.log(error);
            })
            .catch(() => {})
            .finally(() => {
                this.formLoading = false;
            });
        },
        cancel(){
            this.dialogVisible = false;
        },
        iniPartBrand(partType){
            debugger;
            for(var s=0;s<this.partTypeEnum.length;s++){
                if(this.partTypeEnum[s].displayName == partType){
                    this.partBrands = this.partTypeEnum[s].brands;
                    break;
                }
            }
        },
        changePartType(){
            this.iniPartBrand(this.oeDetail.partType);
        },
        changePartType1(){
            this.iniPartBrand(this.addOePartCode.partName);
        },
        deleteOePart(){
            this.$confirm('确认删除此OE号吗？', '提示', {
            confirmButtonText: '确认',
            cancelButtonText: '取消',
            closeOnClickModal: false,
            type: 'warning'
            }).then(() => {
                var deleteRequest = {
                    oePartCode:this.oeDetail.oePartCode
                };
                this.btnDeleteLoading = true;
                appSvc.deleteOePartCode(deleteRequest).then(res=>{
                    var data = res.data;
                    if(data){
                        this.$message({message: "删除成功",type: "success"});
                        this.dialogVisible = false;
                        this.getPaged();
                    }
                    else{
                        this.$message({message: "删除失败",type: "error"});
                    }
                },error=>{
                    console.log(error)
                })
                .catch(()=>{})
                .finally(()=>{
                    this.btnDeleteLoading = false;
                })
            }).catch(()=>{});
        },
        editOePart(){
            var partCodes = [];
            for(var i=0;i<this.oeDetail.partCodes.length;i++){
                if(this.oeDetail.partCodes[i].partCode != ''){
                    partCodes.push(this.oeDetail.partCodes[i]);
                }
            }
            var editRequest = {
                oePartCode:this.oeDetail.oePartCode,
                partName:this.oeDetail.partType,
                vehicleBrand:this.oeDetail.vehicleBrand,
                partCode:partCodes
            };
            this.btnSaveLoading = true;
            appSvc.editOePartCode(editRequest).then(res=>{
                var data = res.data;
                if(data){
                    this.$message({message: "修改成功",type: "success"});
                    this.dialogVisible = false;
                    this.getPaged();
                }
                else{
                    this.$message({message: "修改失败",type: "error"});
                }
            },error=>{
                console.log(error)
            })
            .catch(()=>{})
            .finally(()=>{
                this.btnSaveLoading = false;
            })
        },
        addPartCode(){
            var emptyData = {
                brand:'',
                partCode:''
            };
            this.oeDetail.partCodes.push(emptyData);
        },
        addPartCode1(){
            var emptyData = {
                brand:'',
                partCode:''
            };
            this.addOePartCode.partCode.push(emptyData);
        },
        removePartCode(attaValue){
            this.oeDetail.partCodes.splice(attaValue,1);
        },
        removePartCode1(attaValue){
            this.addOePartCode.partCode.splice(attaValue,1);
        },
        cancel1(){
            this.dialogVisible1 = false;
        },
        addOePart(){
            if(this.addOePartCode.oePartCode == ''){
                this.$message({message: "请输入OE号",type: "warning"});
                return;
            }
            if(this.addOePartCode.partName == ''){
                this.$message({message: "请选择配件类型",type: "warning"});
                return;
            }

            var partCodes = [];
            for(var i=0;i<this.addOePartCode.partCode.length;i++){
                if(this.addOePartCode.partCode[i].partCode != ''){
                    partCodes.push(this.addOePartCode.partCode[i]);
                }
            }
            this.addOePartCode.partCode = partCodes;

            this.btnSaveLoadingA = true;

            appSvc.addOePartCode(this.addOePartCode).then(res=>{
                var data = res.data;
                if(data){
                    this.$message({message: "添加成功",type: "success"});
                    this.dialogVisible1 = false;
                    this.getPaged();
                }
                else{
                    this.$message({message: "添加失败",type: "error"});
                }
            },error=>{
                console.log(error)
            })
            .catch(()=>{})
            .finally(()=>{
                this.btnSaveLoadingA = false;
            })
        }
    }
}
</script>