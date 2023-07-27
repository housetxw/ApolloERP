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
                    <el-select style="width:120px;" v-model="condition.categoryAlias" filterable clearable placeholder="选择一级分类">
                        <el-option
                            v-for="item in categoryConfigList"
                            :key="item.categoryAlias"
                            :label="item.categoryName"
                            :value="item.categoryAlias">
                            </el-option>
                    </el-select>
                </el-form-item>
            </template>
            <template v-slot:header_btn>
                <el-button type="primary" size="mini" icon="el-icon-plus" @click="add">新增</el-button>
            </template>
            <template v-slot:tb_cols>
                <el-table-column label="标识" prop="packageType" :width="60"></el-table-column>
                <el-table-column label="显示名" prop="displayName" :width="120"></el-table-column>
                <el-table-column label="建议" prop="suggestTip" :width="130"></el-table-column>
                <el-table-column label="简要描述" prop="briefDescription"></el-table-column>
                <el-table-column label="服务pid" prop="serviceId"></el-table-column>
                <el-table-column label="一级分类" prop="categoryName" :width="90"></el-table-column>
                <el-table-column label="适配商品类型" prop="productTypeName" :width="100"></el-table-column>
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
                <el-form :model="packageDetail" size="mini" label-width="120px">
                    <el-form-item label="标识">
                        <el-input style="width:300px" :disabled="true" v-model="packageDetail.packageType"></el-input>
                    </el-form-item>
                    <el-form-item label="显示名">
                        <el-input style="width:300px" v-model="packageDetail.displayName"></el-input>
                    </el-form-item>
                    <el-form-item label="建议提示">
                        <el-input style="width:300px" v-model="packageDetail.suggestTip"></el-input>
                    </el-form-item>
                    <el-form-item label="简要描述">
                        <el-input style="width:300px" v-model="packageDetail.briefDescription"></el-input>
                    </el-form-item>
                    <el-form-item label="描述跳转url">
                        <el-input style="width:300px" v-model="packageDetail.descriptionLink"></el-input>
                    </el-form-item>
                    <el-form-item label="详细描述">
                        <el-input style="width:300px" v-model="packageDetail.detailDescription"></el-input>
                    </el-form-item>
                    <el-form-item label="适配商品类型">
                        <el-select style="width:300px" v-model="packageDetail.productType">
                            <el-option
                                v-for="item in productTypeEnum"
                                :key="item.id"
                                :label="item.displayName"
                                :value="item.id">
                            </el-option>
                        </el-select>
                    </el-form-item>
                    <el-form-item label="图片Logo" style="width:200px;">
                        <el-image v-show="packageDetail.imageUrl != ''" @click="routeImageDetail(packageDetail.imageUrl)" style="width: 25px; height: 25px" :src="packageDetail.imageUrl"></el-image>
                        <el-upload style="float:right;" action="http://upload.qiniup.com/" :before-upload="beforeAvatarUpload" :show-file-list="false"
                            accept="image/jpeg, image/png, image/jpg" :http-request="uploadRequest" :data="{pkId:1}">
                            <i class="el-icon-upload"></i>
                        </el-upload>
                    </el-form-item>
                    <el-form-item label="排序序号">
                        <el-input style="width:300px" v-model="packageDetail.displayIndex"></el-input>
                    </el-form-item>
                    <el-form-item label="服务pid">
                        <el-input style="width:300px" v-model="packageDetail.serviceId"></el-input>
                    </el-form-item>
                    <el-form-item label="一级分类">
                        <el-select style="width:300px" v-model="packageDetail.categoryAlias" filterable clearable placeholder="选择一级分类">
                            <el-option
                                v-for="item in categoryConfigList"
                                :key="item.categoryAlias"
                                :label="item.categoryName"
                                :value="item.categoryAlias">
                            </el-option>
                        </el-select>
                    </el-form-item>
                    <el-form-item label="BaoYangType">
                        <el-select style="width:300px" v-model="packageDetail.baoYangType" multiple filterable placeholder="请选择BaoYangType">
                            <el-option
                                v-for="item in baoYangTypeEnum"
                                :key="item.baoYangType"
                                :label="item.displayName"
                                :value="item.baoYangType">
                            </el-option>
                        </el-select>
                    </el-form-item>
                    <el-form-item label="是否禁用">
                        <el-radio v-model="packageDetail.isDeleted" :label="true">禁用</el-radio>
                        <el-radio v-model="packageDetail.isDeleted" :label="false">启用</el-radio>
                    </el-form-item>
                </el-form>
            </template>
            <template v-slot:footer>
                <el-button icon="el-icon-check" :loading="saveLoading" size="mini" type="success" @click="editPackage()">提交</el-button>
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
                <el-form :model="addPackage" size="mini" label-width="120px">
                    <el-form-item label="标识">
                        <el-input style="width:300px" v-model="addPackage.packageType"></el-input>
                    </el-form-item>
                    <el-form-item label="显示名">
                        <el-input style="width:300px" v-model="addPackage.displayName"></el-input>
                    </el-form-item>
                    <el-form-item label="建议提示">
                        <el-input style="width:300px" v-model="addPackage.suggestTip"></el-input>
                    </el-form-item>
                    <el-form-item label="简要描述">
                        <el-input style="width:300px" v-model="addPackage.briefDescription"></el-input>
                    </el-form-item>
                    <el-form-item label="描述跳转url">
                        <el-input style="width:300px" v-model="addPackage.descriptionLink"></el-input>
                    </el-form-item>
                    <el-form-item label="详细描述">
                        <el-input style="width:300px" v-model="addPackage.detailDescription"></el-input>
                    </el-form-item>
                    <el-form-item label="适配商品类型">
                        <el-select style="width:300px" v-model="addPackage.productType">
                            <el-option
                                v-for="item in productTypeEnum"
                                :key="item.id"
                                :label="item.displayName"
                                :value="item.id">
                            </el-option>
                        </el-select>
                    </el-form-item>
                    <el-form-item label="图片Logo" style="width:200px;">
                        <el-image v-show="addPackage.imageUrl != ''" @click="routeImageDetail(addPackage.imageUrl)" style="width: 25px; height: 25px" :src="addPackage.imageUrl"></el-image>
                        <el-upload style="float:right;" action="http://upload.qiniup.com/" :before-upload="beforeAvatarUpload" :show-file-list="false"
                            accept="image/jpeg, image/png, image/jpg" :http-request="uploadRequest" :data="{pkId:2}">
                            <i class="el-icon-upload"></i>
                        </el-upload>
                    </el-form-item>
                    <el-form-item label="排序序号">
                        <el-input style="width:300px" v-model="addPackage.displayIndex"></el-input>
                    </el-form-item>
                    <el-form-item label="服务pid">
                        <el-input style="width:300px" v-model="addPackage.serviceId"></el-input>
                    </el-form-item>
                    <el-form-item label="一级分类">
                        <el-select style="width:300px" v-model="addPackage.categoryAlias" filterable clearable placeholder="选择一级分类">
                            <el-option
                                v-for="item in categoryConfigList"
                                :key="item.categoryAlias"
                                :label="item.categoryName"
                                :value="item.categoryAlias">
                            </el-option>
                        </el-select>
                    </el-form-item>
                    <el-form-item label="BaoYangType">
                        <el-select style="width:300px" v-model="addPackage.baoYangType" multiple filterable placeholder="请选择BaoYangType">
                            <el-option
                                v-for="item in baoYangTypeEnum"
                                :key="item.baoYangType"
                                :label="item.displayName"
                                :value="item.baoYangType">
                            </el-option>
                        </el-select>
                    </el-form-item>
                    <el-form-item label="是否禁用">
                        <el-radio v-model="addPackage.isDeleted" :label="true">禁用</el-radio>
                        <el-radio v-model="addPackage.isDeleted" :label="false">启用</el-radio>
                    </el-form-item>
                </el-form>
            </template>
            <template v-slot:footer>
                <el-button icon="el-icon-check" :loading="saveLoading1" size="mini" type="success" @click="submitPackage()">提交</el-button>
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
    name:'packageconfig',
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
                categoryAlias:''
            },
            tableData: [],
            formTitle:"Package详情",
            dialogVisible:false,
            packageDetail:{
                id:0,
                packageType:'',
                displayName:'',
                suggestTip:'',
                briefDescription:'',
                descriptionLink:'',
                detailDescription:'',
                productType:0,
                productTypeName:'',
                imageUrl:'',
                displayIndex:0,
                serviceId:'',
                baoYangType:[],
                isDeleted:false,
                categoryAlias:''
            },
            productTypeEnum:[
                {id:0,displayName:'套餐'},
                {id:1,displayName:'单品'}
                // {id:2,displayName:'套餐|单品'}
            ],
            isDeletedEnum:[
                {id:0,displayName:'全部状态'},
                {id:1,displayName:'禁用'},
                {id:2,displayName:'启用'}
            ],
            saveLoading:false,
            addPackage:{
                packageType:'',
                displayName:'',
                suggestTip:'',
                briefDescription:'',
                descriptionLink:'',
                detailDescription:'',
                productType:0,
                imageUrl:'',
                displayIndex:0,
                serviceId:'',
                baoYangType:[],
                isDeleted:false,
                categoryAlias:''
            },
            formTitle1:"新增Package",
            dialogVisible1:false,
            saveLoading1:false,
            baoYangTypeEnum:[],
            categoryConfigList:[]
        }
    },
    created() {
        this.fetchData();
        this.getBaoYangTypeEnum();
        this.getBaoYangCategoryConfig();
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
            .getPackageTypeConfig(this.condition)
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
        getBaoYangTypeEnum(){
            appSvc
            .getValidBaoYangTypeConfig()
            .then(
                res => {
                    var data = res.data;
                    this.baoYangTypeEnum = data;
                },
                error => {
                    console.log(error);
                }
            )
            .catch(() => {})
            .finally(() => {});
        },
        getBaoYangCategoryConfig(){
            appSvc
            .getBaoYangCategoryConfig()
            .then(
                res => {
                    var data = res.data;
                    this.categoryConfigList = data;
                },
                error => {
                    console.log(error);
                }
            )
            .catch(() => {})
            .finally(() => {});
        },
        updateStatus(packageId,status){
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
                    id:packageId,
                    isDeleted:status
                };
                appSvc
                .editPackageTypeConfig(updateRequest)
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
        cancel(){
            this.dialogVisible = false;
        },
        routeDetail(detail){
            Object.assign(this.packageDetail,detail);
            this.dialogVisible = true;
        },
        editPackage(){
            var packageIndex = this.packageDetail.displayIndex;
            var r = /^-?\d+$/;　　//正整数
            if(!r.test(packageIndex)){
                this.$message({message: "序号输入有误，请输入整数",type: "warning"});
                return;
            }

            if(this.packageDetail.displayName == ''){
                this.$message({message: "显示名不能为空",type: "warning"});
                return;
            }

            this.saveLoading = true;

            var editRequest = {
                id:this.packageDetail.id,
                packageType:this.packageDetail.packageType,
                displayName:this.packageDetail.displayName,
                suggestTip:this.packageDetail.suggestTip,
                briefDescription:this.packageDetail.briefDescription,
                descriptionLink:this.packageDetail.descriptionLink,
                detailDescription:this.packageDetail.detailDescription,
                productType:this.packageDetail.productType,
                imageUrl:this.packageDetail.imageUrl,
                displayIndex:this.packageDetail.displayIndex,
                serviceId:this.packageDetail.serviceId,
                isDeleted:this.packageDetail.isDeleted,
                baoYangType:this.packageDetail.baoYangType,
                categoryAlias:this.packageDetail.categoryAlias
            };
            appSvc
            .editPackageTypeConfig(editRequest)
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
        routeImageDetail(url){
            window.open(url);
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
                        this.packageDetail.imageUrl = url;
                    }
                    else{
                        this.addPackage.imageUrl = url;
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
        add(){
            this.addPackage = {
                packageType:'',
                displayName:'',
                suggestTip:'',
                briefDescription:'',
                descriptionLink:'',
                detailDescription:'',
                productType:0,
                imageUrl:'',
                displayIndex:0,
                serviceId:'',
                baoYangType:[],
                isDeleted:false,
                categoryAlias:''
            };
            this.dialogVisible1 = true;
        },
        cancel1(){
            this.dialogVisible1 = false;
        },
        submitPackage(){
            var packageIndex = this.addPackage.displayIndex;
            var r = /^-?\d+$/;　　//正整数
            if(!r.test(packageIndex)){
                this.$message({message: "序号输入有误，请输入整数",type: "warning"});
                return;
            }

            if(this.addPackage.packageType == ''){
                this.$message({message: "请输入标识",type: "warning"});
                return;
            }

            if(this.addPackage.displayName == ''){
                this.$message({message: "请输入显示名",type: "warning"});
                return;
            }

            this.saveLoading1 = true;

            var addRequest = {
                packageType:this.addPackage.packageType,
                displayName:this.addPackage.displayName,
                suggestTip:this.addPackage.suggestTip,
                briefDescription:this.addPackage.briefDescription,
                descriptionLink:this.addPackage.descriptionLink,
                detailDescription:this.addPackage.detailDescription,
                productType:this.addPackage.productType,
                imageUrl:this.addPackage.imageUrl,
                displayIndex:this.addPackage.displayIndex,
                serviceId:this.addPackage.serviceId,
                isDeleted:this.addPackage.isDeleted,
                baoYangType:this.addPackage.baoYangType,
                categoryAlias:this.addPackage.categoryAlias
            };

            appSvc
            .addPackageTypeConfig(addRequest)
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