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
                        placeholder="请输入配件号"
                        style="width:150px"
                        prefix-icon="el-icon-search"
                        v-model="condition.partCode">
                    </el-input>
                </el-form-item>
                <el-form-item>
                    <el-input class="input"
                        clearable
                        placeholder="请输入产品id"
                        style="width:180px"
                        prefix-icon="el-icon-search"
                        v-model="condition.pid">
                    </el-input>
                </el-form-item>
                <el-form-item>
                    <el-tooltip class="item" effect="dark" content="关联时间范围" placement="bottom">
                        <el-form-item>
                            <el-date-picker
                                style="width: 130px;"
                                v-model="condition.startDate"
                                type="date"
                                placeholder="开始日期"
                            ></el-date-picker>&nbsp;-&nbsp;
                            <el-date-picker
                                style="width: 130px;"
                                v-model="condition.endDate"
                                type="date"
                                placeholder="结束日期"
                            ></el-date-picker>
                        </el-form-item>
                    </el-tooltip>
                </el-form-item>
            </template>

            <template v-slot:header_btn>
                <el-button type="primary" size="mini" icon="el-icon-plus" @click="add">新增</el-button>
            </template>

            <template v-slot:tb_cols>
                <el-table-column label="配件号" prop="partCode" :width="120"></el-table-column>
                <el-table-column label="配件类型" prop="partType" :width="200"></el-table-column>
                <el-table-column label="产品ID" prop="pid" :width="150"></el-table-column>
                <el-table-column label="产品名称" prop="productName"></el-table-column>
                <el-table-column label="关联时间" prop="relateTime" :width="150"></el-table-column>
                <el-table-column label="关联人" prop="createBy" :width="120"></el-table-column>
                <el-table-column label="操作" width="80px">
                    <template slot-scope="scope">
                        <el-button-group>
                        <el-button @click="deleteProductRef(scope.row.id)" :loading="btnDeleteLoading" type="danger" size="mini">删除</el-button>
                        </el-button-group>
                    </template>
                </el-table-column>
            </template>
        </rg-page>

        <rg-dialog :title="formTitle"
            :visible.sync="dialogVisible"
            :beforeClose="cancel"
            :btnCancel="{label:'关闭',click:(done)=>{cancel()}}"
            :btnRemove="false"
            :footbar="true"
            width="80%"
            maxWidth="600px"
            minWidth="600px">
            <template v-slot:content>
                <el-form :model="addRelation" size="mini" label-width="120px">
                    <el-form-item label="配件号">
                        <el-input style="width:200px" @blur="checkPartCode" @keydown.enter.native="checkPartCode" v-model="addRelation.partCode"></el-input>
                    </el-form-item>
                    <el-form-item label="配件类型">
                        <span>{{addRelation.partName}}</span>
                    </el-form-item>
                    <el-form-item label="产品id">
                        <el-input style="width:200px" @blur="checkProduct" @keydown.enter.native="checkProduct" v-model="addRelation.pid"></el-input>
                    </el-form-item>
                    <el-form-item label="产品名称">
                        <span>{{addRelation.productName}}</span>
                    </el-form-item>
                    <el-form-item label="产品分类">
                        <span>{{addRelation.categoryName}}</span>
                    </el-form-item>
                    <el-form-item label="验证结果">
                        <span :style="{color:addRelation.sucess?'green':'red'}">{{addRelation.result}}</span>
                    </el-form-item>
                </el-form>
            </template>

            <template v-slot:footer>
                <el-button icon="el-icon-check" :disabled="!addRelation.sucess" :loading="saveLoading" size="mini" type="success" @click="addProductRef()">保存</el-button>
            </template>
        </rg-dialog>
    </main>
</template>

<script>
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";
import { appSvc } from "@/api/baoyang/baoyang";
export default {
    name:'productref',
    data(){
        return{
            tableLoading: false,
            currentPage: 1,
            totalList: 0,
            condition:{
                pageIndex: 1,
                pageSize: 20,
                partCode:'',
                pid:'',
                startDate:'',
                endDate:''
            },
            tableData: [],
            btnDeleteLoading:false,
            saveLoading:false,
            formTitle:'新增关联',
            dialogVisible:false,
            addRelation:{
                partCode:'',
                partName:'',
                pid:'',
                productName:'',
                categoryName:'',
                result:'',
                sucess:false
            }
        }
    },
    created() {
        this.fetchData();
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
            .getBaoYangProductRef(this.condition)
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
        deleteProductRef(pkId){
            this.$confirm('确认删除吗？', '提示', {
            confirmButtonText: '确认',
            cancelButtonText: '取消',
            closeOnClickModal: false,
            type: 'warning'
            }).then(() => {
                var deleteRequest = {
                    id:pkId
                };
                this.btnDeleteLoading = true;
                appSvc.deleteBaoYangProductRef(deleteRequest).then(res=>{
                    var data = res.data;
                    if(data){
                        this.$message({message: "删除成功",type: "success"});
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
        cancel(){
            this.dialogVisible = false;
        },
        add(){
            this.addRelation = {
                partCode:'',
                partName:'',
                pid:'',
                productName:'',
                categoryName:'',
                result:'',
                sucess:false
            };
            this.dialogVisible = true;
        },
        checkPartCode(){
            this.addRelation.sucess = false;
            var checkRequest = {
                partCode:this.addRelation.partCode
            };
            appSvc.checkPartCode(checkRequest).then(res=>{
                var data = res.data;
                if(data!=null){
                    if(data.length == 0){
                        this.addRelation.partName = '无数据'
                    }
                    else{
                        this.addRelation.partName = data.join("，");
                    }
                }
                else{
                    this.addRelation.partName = '无数据'
                }
                if(this.addRelation.partCode!=''&&this.addRelation.pid!=''){
                    this.checkPartCodeAndProduct();
                }
            },error=>{
                console.log(error)
            })
            .catch(()=>{})
            .finally(()=>{})
        },
        checkProduct(){
            this.addRelation.sucess = false;
            var checkRequest = {
                pid:this.addRelation.pid
            };
            appSvc.checkProduct(checkRequest).then(res=>{
                var data = res.data;
                if(data!=null){
                    this.addRelation.productName = data.name;
                    this.addRelation.categoryName = data.category;
                }
                else{
                    this.addRelation.productName = '无数据';
                    this.addRelation.categoryName = '无数据';
                }
                if(this.addRelation.partCode!=''&&this.addRelation.pid!=''){
                    this.checkPartCodeAndProduct();
                }
            },error=>{
                console.log(error)
            })
            .catch(()=>{})
            .finally(()=>{})
        },
        checkPartCodeAndProduct(){
            var cRequest = {
                partCode:this.addRelation.partCode,
                pid:this.addRelation.pid
            };
            appSvc.checkPartCodeAndProduct(cRequest).then(res=>{
                var data = res.data;
                this.addRelation.sucess = data.isSuccess;
                this.addRelation.result = data.message;
            },error=>{
                console.log(error)
            })
            .catch(()=>{})
            .finally(()=>{})
        },
        addProductRef(){
            if(this.addRelation.partCode == ''){
                this.$message({message: "请输入配件号",type: "warning"});
                return;
            }
            if(this.addRelation.pid == ''){
                this.$message({message: "请输入产品id",type: "warning"});
                return;
            }

            var partCodes = [];
            var itemPart = {
                pid:this.addRelation.pid,
                partCode:this.addRelation.partCode
            }
            partCodes.push(itemPart);
            var addRequest = {
                partProductRef:partCodes
            };
            this.saveLoading = true;
            appSvc.insertPartsAssociation(addRequest).then(res=>{
                var data = res.data;
                if(data){
                    this.$message({message: "添加成功",type: "success"});
                    this.dialogVisible = false;
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
                this.saveLoading = false;
            })
        }
    }
}
</script>