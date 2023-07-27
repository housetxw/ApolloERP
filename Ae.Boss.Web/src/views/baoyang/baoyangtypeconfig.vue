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
                        placeholder="请输入显示名"
                        style="width:150px"
                        prefix-icon="el-icon-search"
                        v-model="condition.displayName">
                    </el-input>
                </el-form-item>
                <el-form-item>
                    <el-select style="width:100px;" v-model="condition.isDeleted" filterable placeholder="-是否禁用-">
                        <el-option
                            v-for="item in isDeletedEnum"
                            :key="item.id"
                            :label="item.displayName"
                            :value="item.id">
                            </el-option>
                    </el-select>
                </el-form-item>
                <el-form-item>
                    <el-select style="width:100px;" v-model="condition.typeGroup" filterable placeholder="-请选择组别-">
                        <el-option
                            v-for="item in typeGroupEnum"
                            :key="item.id"
                            :label="item.displayName"
                            :value="item.id">
                            </el-option>
                    </el-select>
                </el-form-item>
            </template>
            <template v-slot:header_btn>
                <el-button type="primary" size="mini" icon="el-icon-plus" @click="add">新增</el-button>
            </template>
            <template v-slot:tb_cols>
                <el-table-column label="标识" prop="baoYangType" :width="60"></el-table-column>
                <el-table-column label="显示名" prop="displayName" :width="120"></el-table-column>
                <el-table-column label="组别" prop="typeGroupDisplay" :width="80"></el-table-column>
                <el-table-column label="产品类目Id" prop="categoryName" :width="100"></el-table-column>
                <el-table-column label="产品三级类目Id" prop="childCategories"></el-table-column>
                <el-table-column label="Icon" :width="80">
                    <template slot-scope="scope">
                        <el-image v-show="scope.row.imageUrl != ''" @click="routeImageDetail(scope.row.imageUrl)" style="width: 25px; height: 25px" :src="scope.row.imageUrl"></el-image>
                    </template>
                </el-table-column>
                <el-table-column label="创建人" prop="createBy"></el-table-column>
                <el-table-column label="创建时间" prop="createTime"></el-table-column>
                <el-table-column label="最后更新人" prop="updateBy"></el-table-column>
                <el-table-column label="最后更新时间" prop="updateTime"></el-table-column>
                <el-table-column label="是否禁用" :width="80">
                    <template slot-scope="scope">
                        <el-tag
                            :type="scope.row.isDeleted === false ? '' : 'danger'"
                        >{{scope.row.isDeleted === true ? '是' : '否'}}</el-tag>
                    </template>
                </el-table-column>
                <el-table-column label="操作" :width="100">
                    <template slot-scope="scope">
                        <el-button-group>
                            <el-button v-show="scope.row.isDeleted==false" @click="updateStatus(scope.row.id,!scope.row.isDeleted)" type="danger" size="mini">禁用</el-button>
                            <el-button v-show="scope.row.isDeleted==true" @click="updateStatus(scope.row.id,!scope.row.isDeleted)" type="info" size="mini">启用</el-button>
                            <el-button @click="routeDetail(scope.row)" type="primary" size="mini">详情</el-button>
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
                <el-form :model="baoYangDetail" size="mini" label-width="120px">
                    <el-form-item label="标识">
                        <el-input style="width:300px" :disabled="true" v-model="baoYangDetail.baoYangType"></el-input>
                    </el-form-item>
                    <el-form-item label="显示名">
                        <el-input style="width:300px" v-model="baoYangDetail.displayName"></el-input>
                    </el-form-item>
                    <el-form-item label="组别">
                        <el-select style="width:300px" v-model="baoYangDetail.typeGroup">
                            <el-option
                                v-for="item in typeGroupEnum1"
                                :key="item.id"
                                :label="item.displayName"
                                :value="item.id">
                            </el-option>
                        </el-select>
                    </el-form-item>
                    <el-form-item label="产品类目Id">
                        <el-input style="width:300px" v-model="baoYangDetail.categoryName"></el-input>
                    </el-form-item>
                    <el-form-item label="产品三级类目Id">
                        <el-input style="width:300px" v-model="baoYangDetail.childCategories" placeholder="请输入产品三级类目，英文逗号分隔"></el-input>
                    </el-form-item>
                    <el-form-item label="图片Icon" style="width:200px;">
                        <el-image v-show="baoYangDetail.imageUrl != ''" @click="routeImageDetail(baoYangDetail.imageUrl)" style="width: 25px; height: 25px" :src="baoYangDetail.imageUrl"></el-image>
                        <el-upload style="float:right;" action="http://upload.qiniup.com/" :before-upload="beforeAvatarUpload" :show-file-list="false"
                            accept="image/jpeg, image/png, image/jpg" :http-request="uploadRequest" :data="{pkId:1}">
                            <i class="el-icon-upload"></i>
                        </el-upload>
                    </el-form-item>
                     <el-form-item label="是否禁用">
                        <el-radio v-model="baoYangDetail.isDeleted" :label="true">禁用</el-radio>
                        <el-radio v-model="baoYangDetail.isDeleted" :label="false">启用</el-radio>
                    </el-form-item>
                </el-form>
            </template>
            <template v-slot:footer>
                <el-button icon="el-icon-check" :loading="saveLoading" size="mini" type="success" @click="editBaoYangType()">提交</el-button>
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
                <el-form :model="addBaoYangType" size="mini" label-width="120px">
                    <el-form-item label="标识">
                        <el-input style="width:300px" v-model="addBaoYangType.baoYangType"></el-input>
                    </el-form-item>
                    <el-form-item label="显示名">
                        <el-input style="width:300px" v-model="addBaoYangType.displayName"></el-input>
                    </el-form-item>
                    <el-form-item label="组别">
                        <el-select style="width:300px" v-model="addBaoYangType.typeGroup">
                            <el-option
                                v-for="item in typeGroupEnum1"
                                :key="item.id"
                                :label="item.displayName"
                                :value="item.id">
                            </el-option>
                        </el-select>
                    </el-form-item>
                    <el-form-item label="产品类目Id">
                        <el-input style="width:300px" v-model="addBaoYangType.categoryName"></el-input>
                    </el-form-item>
                    <el-form-item label="产品三级类目Id">
                        <el-input style="width:300px" v-model="addBaoYangType.childCategories" placeholder="请输入产品三级类目，英文逗号分隔"></el-input>
                    </el-form-item>
                    <el-form-item label="图片Icon" style="width:200px;">
                        <el-image v-show="addBaoYangType.imageUrl != ''" @click="routeImageDetail(addBaoYangType.imageUrl)" style="width: 25px; height: 25px" :src="addBaoYangType.imageUrl"></el-image>
                        <el-upload style="float:right;" action="http://upload.qiniup.com/" :before-upload="beforeAvatarUpload" :show-file-list="false"
                            accept="image/jpeg, image/png, image/jpg" :http-request="uploadRequest" :data="{pkId:2}">
                            <i class="el-icon-upload"></i>
                        </el-upload>
                    </el-form-item>
                     <el-form-item label="是否禁用">
                        <el-radio v-model="addBaoYangType.isDeleted" :label="true">禁用</el-radio>
                        <el-radio v-model="addBaoYangType.isDeleted" :label="false">启用</el-radio>
                    </el-form-item>
                </el-form>
            </template>
            <template v-slot:footer>
                <el-button icon="el-icon-check" :loading="saveLoading1" size="mini" type="success" @click="submitBaoYangType()">提交</el-button>
            </template>
        </rg-dialog>
    </main>
</template>

<script>
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";
import { appSvc } from "@/api/baoyang/baoyang";
import { upload } from '@/utils/uploadfile'
export default {
    name:'baoyangtypeconfig',
    data(){
        return{
            tableLoading: false,
            currentPage: 1,
            totalList: 0,
            condition:{
                pageIndex: 1,
                pageSize: 20,
                displayName:'',
                isDeleted:0,
                typeGroup:''
            },
            tableData: [],
            isDeletedEnum:[
                {id:0,displayName:'全部状态'},
                {id:1,displayName:'禁用'},
                {id:2,displayName:'启用'}
            ],
            typeGroupEnum:[
                {id:"",displayName:'全部组别'},
                {id:"Accessory",displayName:'油液类'},
                {id:"Part",displayName:'配件类'},
                {id:"Package",displayName:'套餐类'},
                {id:"Maintainence",displayName:'维保类'}
            ],
            typeGroupEnum1:[
                {id:"Accessory",displayName:'油液类'},
                {id:"Part",displayName:'配件类'},
                {id:"Package",displayName:'套餐类'},
                {id:"Maintainence",displayName:'维保类'}
            ],
            baoYangDetail:{
                id:0,
                baoYangType:'',
                displayName:'',
                categoryName:'',
                childCategories:'',
                typeGroup:'',
                typeGroupDisplay:'',
                imageUrl:'',
                isDeleted:false,
                createBy:'',
                createTime:'',
                updateBy:'',
                updateTime:''
            },
            formTitle:'BaoYangType详情',
            dialogVisible:false,
            addBaoYangType:{
                baoYangType:'',
                displayName:'',
                categoryName:'',
                childCategories:'',
                typeGroup:'',
                imageUrl:'',
                isDeleted:false
            },
            saveLoading:false,
            formTitle1:'新增BaoYangType',
            dialogVisible1:false,
            saveLoading1:false
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
            .getBaoYangTypeConfig(this.condition)
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
            this.addBaoYangType = {
                baoYangType:'',
                displayName:'',
                categoryName:'',
                childCategories:'',
                typeGroup:'',
                imageUrl:'',
                isDeleted:false
            };
            this.dialogVisible1 = true;
        },
        routeImageDetail(url){
            window.open(url);
        },
        updateStatus(baoYangId,status){
            var msg="禁用";
            if(!status){
                msg="启用";
            }
            this.$confirm('确定要'+msg+'吗?', '提示', {
            confirmButtonText: '确定',
            cancelButtonText: '取消',
            closeOnClickModal: false,
            type: 'warning'
            }).then(() => {
                var updateRequest={
                    id:baoYangId,
                    isDeleted:status
                };
                appSvc
                .editBaoYangTypeConfig(updateRequest)
                .then(
                    res => {
                        var data = res.data;
                        if(data){
                            this.$message({ message:"操作成功", type: "success" });
                        }
                        else{
                            this.$message({ message:res.message, type: "error" });
                        }
                        this.getPaged();
                    },
                    error => {
                        console.log(error);
                    }
                )
                .catch(() => {})
                .finally(() => {});
            }).catch(()=>{});
        },
        routeDetail(detail){
            Object.assign(this.baoYangDetail,detail);
            this.dialogVisible = true;
        },
        cancel(){
            this.dialogVisible = false;
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
                    if(index == 1){
                        this.baoYangDetail.imageUrl = url;
                    }
                    else{
                        this.addBaoYangType.imageUrl = url;
                    }
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
        },
        editBaoYangType(){
            if(this.baoYangDetail.displayName == ''){
                this.$message({message: "显示名不能为空",type: "warning"});
                return;
            }

            if(this.baoYangDetail.typeGroup == ''){
                this.$message({message: "组别不能为空",type: "warning"});
                return;
            }

            this.saveLoading = true;
            var editRequest = {
                id:this.baoYangDetail.id,
                displayName:this.baoYangDetail.displayName,
                categoryName:this.baoYangDetail.categoryName,
                childCategories:this.baoYangDetail.childCategories,
                typeGroup:this.baoYangDetail.typeGroup,
                imageUrl:this.baoYangDetail.imageUrl,
                isDeleted:this.baoYangDetail.isDeleted
            };
            appSvc
            .editBaoYangTypeConfig(editRequest)
            .then(
                res => {
                    var data = res.data;
                    if(data){
                        this.$message({ message:"操作成功", type: "success" });
                        this.dialogVisible = false;
                        this.getPaged();
                    }
                    else{
                        this.$message({ message:res.message, type: "error" });
                    }
                },
                error => {
                    console.log(error);
                }
            )
            .catch(() => {})
            .finally(() => {
                this.saveLoading = false;
            });
        },
        cancel1(){
            this.dialogVisible1 = false;
        },
        submitBaoYangType(){
            if(this.addBaoYangType.baoYangType == ''){
                this.$message({message: "标识不能为空",type: "warning"});
                return;
            }

            if(this.addBaoYangType.displayName == ''){
                this.$message({message: "显示名不能为空",type: "warning"});
                return;
            }

            if(this.addBaoYangType.typeGroup == ''){
                this.$message({message: "组别不能为空",type: "warning"});
                return;
            }

            var addRequest={
                baoYangType:this.addBaoYangType.baoYangType,
                displayName:this.addBaoYangType.displayName,
                categoryName:this.addBaoYangType.categoryName,
                childCategories:this.addBaoYangType.childCategories,
                typeGroup:this.addBaoYangType.typeGroup,
                imageUrl:this.addBaoYangType.imageUrl,
                isDeleted:this.addBaoYangType.isDeleted
            };

            this.saveLoading1 = true;

            appSvc
            .addBaoYangTypeConfig(addRequest)
            .then(
                res => {
                    var data = res.data;
                    if(data){
                        this.$message({ message:"新增成功", type: "success" });
                        this.dialogVisible1 = false;
                        this.getPaged();
                    }
                    else{
                        this.$message({ message:res.message, type: "error" });
                    }
                },
                error => {
                    console.log(error);
                }
            )
            .catch(() => {})
            .finally(() => {
                this.saveLoading1 = false;
            });
        }
    }
}
</script>