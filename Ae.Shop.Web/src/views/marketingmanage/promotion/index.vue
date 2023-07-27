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

            <!-- 搜索 -->
            <template v-slot:condition>
                <el-form-item>
                <el-input
                    class="input"
                    clearable
                    placeholder="请输入活动名称"
                    style="width:150px"
                    prefix-icon="el-icon-search"
                    v-model="condition.activityName"
                ></el-input>
                </el-form-item>
                <el-form-item>
                <el-input
                    class="input"
                    clearable
                    placeholder="请输入活动Id"
                    style="width:180px"
                    prefix-icon="el-icon-search"
                    v-model="condition.activityId"
                ></el-input>
                </el-form-item>
                <el-form-item>
                <el-input
                    class="input"
                    clearable
                    placeholder="请输入商品Pid"
                    style="width:150px"
                    prefix-icon="el-icon-search"
                    v-model="condition.productCode"
                ></el-input>
                </el-form-item>
                <el-form-item>
                    <el-tooltip class="item" effect="dark" content="活动时间范围" placement="bottom">
                        <el-form-item>
                        <el-date-picker
                            style="width: 128px;"
                            v-model="condition.startDate"
                            type="date"
                            placeholder="开始日期"
                        ></el-date-picker>&nbsp;-&nbsp;
                        <el-date-picker
                            style="width: 128px;"
                            v-model="condition.endDate"
                            type="date"
                            placeholder="结束日期"
                        ></el-date-picker>
                        </el-form-item>
                    </el-tooltip>
                </el-form-item>
                <el-form-item>
                    <el-select v-model="condition.status" style="width:100px;">
                        <el-option
                        v-for="item in promotionStatus"
                        :key="item.value"
                        :label="item.displayName"
                        :value="item.value">
                        </el-option>
                    </el-select>
                </el-form-item>
            </template>

            <template v-slot:header_btn>
                <el-button type="primary" size="mini" icon="el-icon-plus" @click="add">新增活动</el-button>
            </template>

            <!-- 数据表格 -->
            <template v-slot:tb_cols>
                <el-table-column label="促销活动Id" prop="activityId" :width="240"></el-table-column>
                <rg-table-column label="活动名称" prop="name" />
                <rg-table-column label="促销语" prop="subtitle" />
                <el-table-column label="开始时间" prop="startTime" :width="130"></el-table-column>
                <el-table-column label="结束时间" prop="endTime" :width="130"></el-table-column>
                <el-table-column label="活动状态" prop="statusDisplay" :width="80"></el-table-column>
                <el-table-column label="创建人" prop="createdBy" :width="100"></el-table-column>
                <el-table-column fixed="right" label="操作" :width="90">
                    <template slot-scope="scope">
                        <el-button-group>
                        <el-button @click="routeDetail(scope.row.activityId)" type="primary" size="mini">查看详情</el-button>
                        </el-button-group>
                    </template>
                </el-table-column>
            </template>
        </rg-page>

        <rg-dialog :title="formTitle"
            :visible.sync="dialogVisible"
            :beforeClose="cancel"
            :btnCancel="{label:'关闭',click:(done)=>{cancel()}}"
            :btnRemove=false
            :footbar="true"
            width="80%"
            maxWidth="1200px"
            minWidth="800px">
            <template v-slot:content>
                <el-form :model="addRequest"
                    :rules="rules"
                    ref="formModel"
                    size="mini"
                    label-width="120px">
                    <el-input style="width:300px;margin-left:120px;" prefix-icon="el-icon-search" clearable size="mini" placeholder="输入商品Pid，回车键搜索" v-model="searchPid" @keydown.enter.native="searchProduct(this);"></el-input>
                    <el-divider content-position="left">
                        促销活动配置
                    </el-divider>
                    <el-form-item label="活动名称" required>
                        <el-input style="width:500px;" v-model="addRequest.name"></el-input>
                    </el-form-item>
                    <el-form-item label="促销描述" required>
                        <el-input style="width:500px;" v-model="addRequest.subtitle"></el-input>
                    </el-form-item>
                    <el-form-item label="活动时间" required>
                        <el-date-picker style="width:300px;"
                            v-model="rangeDate"
                            type="daterange"
                            range-separator="至"
                            start-placeholder="开始日期"
                            end-placeholder="结束日期"
                            :default-time="['00:00:00', '00:00:00']">
                        </el-date-picker>
                    </el-form-item>
                    <el-form-item label="促销渠道" required>
                        <el-checkbox-group v-model="addRequest.checkedChannel" style="margin-top:5px;">
                            <el-checkbox v-for="channel in channelOptions" :label="channel.value" :key="channel.value">{{channel.name}}</el-checkbox>
                        </el-checkbox-group>
                    </el-form-item>
                    <el-form-item label="标签名称" required>
                        <el-radio style="margin-top:10px;" v-model="radio" @change="changeLabel()" label="1">默认(促销)</el-radio>
                        <el-radio style="margin-top:10px;" v-model="radio" @change="changeLabel()" label="2">自定义</el-radio>
                        <el-input v-show="showTag" style="width:120px;margin-left:-20px;" clearable placeholder="自定义标签" v-model="addRequest.label"></el-input>
                    </el-form-item>
                    <el-form-item label="活动类型" required>
                        <el-radio style="margin-top:10px;" v-model="radioPromotionType" label="0">自定义促销价</el-radio>
                    </el-form-item>
                    <el-form-item label="活动描述" required>
                        <Tinymce ref="content" v-model="addRequest.description" :height="350" />
                    </el-form-item>
                    <el-divider content-position="left">
                        选择促销商品 
                        <el-button size="mini" type="primary" @click="showProductDialog" v-show="productData.length==0">添加商品</el-button>
                    </el-divider>
                    <el-table size="mini" :data="productData" border style="width: 100%;margin-top: 20px">
                        <el-table-column label="商品Pid" prop="productCode"></el-table-column>
                        <el-table-column label="商品名称" prop="productName"></el-table-column>
                        <el-table-column label="售价" prop="price"></el-table-column>
                        <el-table-column label="促销价">
                            <template slot-scope="scope">
                                <el-input clearable size="mini" placeholder="输入促销价" v-model="scope.row.promotionPrice"></el-input>
                            </template>
                        </el-table-column>
                        <el-table-column label="限购数量">
                            <template slot-scope="scope">
                                <el-input clearable size="mini" placeholder="输入限购数量" v-model="scope.row.limitQuantity"></el-input>
                            </template>
                        </el-table-column>
                        <el-table-column label="操作" width="80px">
                            <template>
                                <el-button-group>
                                <el-button @click="showProductDialog" type="primary" size="mini">更换商品</el-button>
                                </el-button-group>
                            </template>
                        </el-table-column>
                    </el-table>
                </el-form>
            </template>
            <template v-slot:footer>
                <el-button
                    icon="el-icon-check"
                    size="mini"
                    type="primary"
                    :loading="btnSaveLoadingA"
                    @click="addActivity()"
                >提交</el-button>
            </template>
        </rg-dialog>

        <rg-dialog :title="formTitle1"
            :visible.sync="dialogVisible1"
            :beforeClose="()=>{this.dialogVisible1=false}"
            :btnCancel="{label:'关闭',click:(done)=>{this.dialogVisible1=false;}}"
            :btnRemove="false"
            :footbar="true"
            :loading="formLoading"
            width="80%"
            maxWidth="1200px"
            minWidth="800px">
            <template v-slot:content>
                <el-form :model="activityDetail"
                    size="mini"
                    :disabled="!showSave"
                    label-width="120px">
                    <el-divider content-position="left">
                        促销活动配置
                        <span style="color:red;font-size:12px;font-weight:bold;">（状态：{{activityDetail.statusDisplay}}）</span>
                    </el-divider>
                    <el-form-item label="活动名称" required>
                        <el-input style="width:500px;" v-model="activityDetail.name"></el-input>
                    </el-form-item>
                    <el-form-item label="促销描述" required>
                        <el-input style="width:500px;" v-model="activityDetail.subtitle"></el-input>
                    </el-form-item>
                    <el-form-item label="活动时间" required>
                        <el-date-picker style="width:300px;"
                            v-model="activityDate"
                            type="daterange"
                            range-separator="至"
                            start-placeholder="开始日期"
                            end-placeholder="结束日期"
                            :default-time="['00:00:00', '00:00:00']">
                        </el-date-picker>
                    </el-form-item>
                    <el-form-item label="促销渠道" required>
                        <el-checkbox-group v-model="activityDetail.promotionChannel" style="margin-top:5px;">
                            <el-checkbox v-for="channel in channelOptions" :label="channel.value" :key="channel.value">{{channel.name}}</el-checkbox>
                        </el-checkbox-group>
                    </el-form-item>
                    <el-form-item label="标签名称" required>
                        <el-radio style="margin-top:10px;" v-model="radio1" @change="changeLabel1()" label="1">默认(促销)</el-radio>
                        <el-radio style="margin-top:10px;" v-model="radio1" @change="changeLabel1()" label="2">自定义</el-radio>
                        <el-input v-show="showTag1" style="width:120px;margin-left:-20px;" clearable placeholder="自定义标签" v-model="inputLabel"></el-input>
                    </el-form-item>
                    <el-form-item label="活动类型" required>
                        <el-radio style="margin-top:10px;" v-model="activityDetail.promotionType" label="0">自定义促销价</el-radio>
                    </el-form-item>
                    <el-form-item label="活动描述" required>
                        <Tinymce ref="contentE" v-model="activityDetail.description" :height="350" />
                    </el-form-item>
                    <el-divider content-position="left">选择促销商品 </el-divider>
                    <el-table size="mini" :data="activityDetail.products" border style="width: 100%;margin-top: 20px">
                        <el-table-column label="商品Pid" prop="productCode"></el-table-column>
                        <el-table-column label="商品名称" prop="productName"></el-table-column>
                        <el-table-column label="售价" prop="price"></el-table-column>
                        <el-table-column label="促销价">
                            <template slot-scope="scope">
                                <el-input clearable size="mini" placeholder="输入促销价" v-model="scope.row.promotionPrice"></el-input>
                            </template>
                        </el-table-column>
                        <el-table-column label="限购数量">
                            <template slot-scope="scope">
                                <el-input clearable size="mini" placeholder="输入限购数量" v-model="scope.row.limitQuantity"></el-input>
                            </template>
                        </el-table-column>
                        <el-table-column label="操作" width="80px">
                            <template>
                                <el-button-group>
                                <el-button @click="showProductDialog" type="primary" size="mini">更换商品</el-button>
                                </el-button-group>
                            </template>
                        </el-table-column>
                    </el-table>
                </el-form>
            </template>
            <template v-slot:footer>
                <el-button
                    icon="el-icon-delete"
                    size="mini"
                    type="danger"
                    v-show="showFinish"
                    :loading="btnSaveLoadingF"
                    @click="finishActivity()"
                >结束活动</el-button>
                <el-button
                    icon="el-icon-check"
                    size="mini"
                    type="primary"
                    v-show="showSave"
                    :loading="btnSaveLoadingE"
                    @click="editActivity()"
                >保存</el-button>
            </template>
        </rg-dialog>

        <rg-dialog :title="formTitle2"
            :visible.sync="dialogVisible2"
            :beforeClose="()=>{this.dialogVisible2=false;}"
            :btnCancel="{label:'关闭',click:(done)=>{this.dialogVisible2=false;}}"
            :btnRemove="false"
            :footbar="true"
            width="80%"
            maxWidth="800px"
            minWidth="800px"
            minHeight="500px"
            maxHeight="500px">
            <template v-slot:content>
                <el-input style="width:200px;" prefix-icon="el-icon-search" clearable size="mini" placeholder="输入商品名称或Pid" v-model="searchPid1" @keydown.enter.native="queryProduct(this);"></el-input>
                <el-button
                    size="mini"
                    type="primary"
                    @click="queryProduct()"
                >搜索</el-button>
                <el-table size="mini" height="320" v-loading="tableLoading1" :data="productResult" border style="width: 100%;margin-top: 10px;overflow-y:auto;">
                    <el-table-column label="操作" width="60px">
                        <template slot-scope="scope">
                            <el-link @click="chooseProduct(scope.row)" size="mini" type="primary">选择</el-link>
                        </template>
                    </el-table-column>
                    <el-table-column label="商品Pid" prop="productCode"></el-table-column>
                    <el-table-column label="商品名称" prop="productName"></el-table-column>
                    <el-table-column label="描述" prop="description"></el-table-column>
                    <el-table-column label="售价" prop="salesPrice" width="120px"></el-table-column>
                    <el-table-column label="上下架状态" width="120px">
                        <template slot-scope="scope">
                            <el-tag v-if="scope.row.onSale==1">
                                上架中
                            </el-tag>
                            <el-tag type="danger" v-else-if="scope.row.onSale==0">
                                下架
                            </el-tag>		
                        </template>
                    </el-table-column>
                </el-table>
            </template>
        </rg-dialog>
    </main>
</template>

<script>
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";
import { appSvc } from "@/api/marketingmanage/promotion";
import Tinymce from "@/components/Tinymce";
export default {
    name: "promotion",
    components: {  Tinymce },
    data(){
        return{
            showAddProduct:false,
            addOrDteailPage:0,
            searchPid:'',
            searchPid1:'',
            formTitle:"新增促销",
            tableLoading: false,
            currentPage: 1,
            totalList: 0,
            condition: {
                pageIndex: 1,
                pageSize: 20,
                activityName:'',
                activityId:'',
                productCode:'',
                startDate:'',
                endDate:'',
                status:0
            },
            tableData: [],
           
            promotionStatus:[
                {
                    displayName:'全部状态',
                    value:0
                },
                {
                    displayName:'进行中',
                    value:4
                },{
                    displayName:'未开始',
                    value:3
                },{
                    displayName:'已结束',
                    value:5
                },{
                    displayName:'待审核',
                    value:1
                },{
                    displayName:'已拒绝',
                    value:2
                }
            ],
            dialogVisible:false,
            btnSaveLoadingA:false,
            channelOptions:[{
                value:'MiniApp',
                name:'微信小程序'
            },
            {
                value:'ShopApp',
                name:'门店管家'
            }],
            addRequest:{
                name:'',
                subtitle:'',
                description:'',
                label:'',
                thumbUrl:'',
                checkedChannel:[],
                promotionType:'0'
            },
            productData:[],
            radio:"1",
            radioPromotionType:'0',
            rangeDate:[],
            rules: {
            },
            showTag:false,
            formTitle1:"促销详情",
            dialogVisible1:false,
            showFinish:false,
            showSave:false,
            activityDetail:{
                activityId:'',
                name:'',
                subtitle:'',
                startTime:'',
                endTime:'',
                statusDisplay:'',
                thumbUrl:'',
                status:0,
                label:'',
                promotionChannel:[],
                description:'',
                promotionType:"0",
                products:[]
            },
            activityDate:[],
            radio1:"0",
            showTag1:false,
            inputLabel:'',
            formLoading:false,
            btnSaveLoadingF:false,
            btnSaveLoadingE:false,
            formTitle2:'搜索商品',
            dialogVisible2:false,
            tableLoading1:false,
            productResult:[]
        }
    },
    created() {
        this.fetchData();
    },
    methods:{
        changeLabel(){
            if(this.radio == 2){
                this.showTag=true;
            }
            else{
                this.showTag=false;
            }
        },
        changeLabel1(){
            if(this.radio1 == 2){
                this.showTag1 = true;
            }
            else{
                this.showTag1 = false;
            }
        },
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
            .searchPromotionActivity(this.condition)
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
            this.dialogVisible = true;
            this.addOrDteailPage = 0;
        },
        routeDetail(paraId){
            var detailRequest = {
                activityId:paraId
            };
            this.formLoading = true;
            appSvc
            .getPromotionActivityDetail(detailRequest)
            .then(
            res => {
                var data = res.data;
                if(data == null){
                    this.$message({
                        message: "系统异常，请重试！",
                        type: "error"
                    });
                    return;
                }
                this.activityDetail = data;
                this.activityDate = new Array(data.startTime,data.endTime);
                if(data.label == '促销'){
                    this.radio1 = "1";
                    this.inputLabel = '';
                    this.showTag1 = false;
                }
                else{
                    this.radio1 = "2";
                    this.inputLabel = data.label;
                    this.showTag1 = true;
                }
                if(data.status == 4){
                    this.showFinish = true;
                }
                else{
                    this.showFinish = false;
                }
                if(data.status == 1){
                    this.showSave = true
                }
                else{
                    this.showSave = false
                }
                this.dialogVisible1 = true;
                this.addOrDteailPage = 1;
                this.$refs.contentE.setContent(data.description);
            },
            error => {
                console.log(error);
            }
            )
            .catch(() => {})
            .finally(() => {
                this.formLoading = false;
            });
        },
        //搜索商品
        searchProduct(o){
            if(this.searchPid==''){
                return;
            }
            var searchProductRequest = {
                shopProductCode:this.searchPid
            };
            appSvc
            .getShopProductByCode(searchProductRequest)
            .then(
                res => {
                    var data = res.data;
                    if(data != null){
                        this.addRequest.name = data.productName;
                        this.addRequest.subtitle = data.description;
                        this.addRequest.thumbUrl = data.icon;
                        this.productData = [{
                            productCode:data.productCode,
                            productName:data.productName,
                            price:data.salesPrice,
                            promotionPrice:'',
                            limitQuantity:''
                        }];
                        this.shopId = [data.shopId];
                    }
                    else{
                        this.$message({message: "输入商品不存在",type: "warning"});
                    }
                },
                error => {
                    console.log(error);
                }
            )
            .catch(() => {})
            .finally(() => {});
        },
        cancel(){
            this.dialogVisible=false;
            this.$refs.content.setContent('');
            this.searchPid = '';
            this.rangeDate = [];
            this.addRequest = {
                name:'',
                subtitle:'',
                description:'',
                label:'',
                thumbUrl:'',
                checkedChannel:[],
                promotionType:'0'
            };
            this.radio = '1';
            this.productData = [];
            this.showTag = false;
        },
        addActivity(){
            var label = "促销";
            if(this.radio == "2"){
                label = this.addRequest.label;
            }
            if(this.addRequest.name==''){
                this.$message({message: "请输入活动名称",type: "warning"});
                return;
            }
            if(this.addRequest.subtitle==''){
                this.$message({message: "请输入促销描述",type: "warning"});
                return;
            }
            if(this.addRequest.description==''){
                this.$message({message: "请输入活动描述",type: "warning"});
                return;
            }
            if(label==''){
                this.$message({message: "请输入标签名称",type: "warning"});
                return;
            }
            if(this.addRequest.checkedChannel.length==0){
                this.$message({message: "请选择促销渠道",type: "warning"});
                return;
            }
            if(this.rangeDate.length == 0){
                this.$message({message: "请选择活动时间",type: "warning"});
                return;
            }

            if(this.productData.length == 0){
                this.$message({message: "请选择促销商品",type: "warning"});
                return;
            }

            for(var i=0;i<this.productData.length;i++){
                var s = /(^[1-9](\d+)?(\.\d{1,2})?$)|(^\d\.\d{1,2}$)/ ;
                var r = /^\+?[1-9][0-9]*$/;　　//正整数
                var salePrice = this.productData[i].price;
                var promotionPrice = this.productData[i].promotionPrice;
                var itemLimit = this.productData[i].limitQuantity;
                if(!s.test(promotionPrice)){
                    this.$message({message: "第"+ (i+1)+"行商品促销价输入有误，大于0且最多2位小数！",type: "warning"});
                    return;
                }
                if(parseFloat(salePrice)<parseFloat(promotionPrice)){
                    this.$message({message: "第"+ (i+1)+"行商品促销价输入有误，促销价不能大于销售价！",type: "warning"});
                    return;
                }
                var flag = r.test(itemLimit);
                if(!flag){
                    this.$message({message: "第"+ (i+1)+"行商品限购数量输入有误，请输入正整数！",type: "warning"});
                    return;
                }
            }

            var startDate = this.rangeDate[0].toLocaleDateString();
            var endDate = this.rangeDate[1].toLocaleDateString();
            this.btnSaveLoadingA = true;
            var submitRequest = {
                name:this.addRequest.name,
                subtitle:this.addRequest.subtitle,
                description:this.addRequest.description,
                thumbUrl:this.addRequest.thumbUrl,
                activityType:0,
                promotionType:this.radioPromotionType,
                label:label,
                startTime:startDate,
                endTime:endDate,
                applyChannel:this.addRequest.checkedChannel,
                activityProduct:this.productData
            };
            appSvc
            .addPromotionActivity(submitRequest)
            .then(
                res => {
                    var data = res.data;
                    this.$message({message: "提交成功！",type: "success"});
                    this.dialogVisible = false;
                    this.getPaged(false);
                },
                error => {
                    console.log(error);
                }
            )
            .catch(() => {})
            .finally(() => {
                this.btnSaveLoadingA = false;
            });
        },
        editActivity(){
            var label = "促销";
            if(this.radio1 == "2"){
                label = this.inputLabel;
            }
            if(this.activityDetail.name==''){
                this.$message({message: "请输入活动名称",type: "warning"});
                return;
            }
            if(this.activityDetail.subtitle==''){
                this.$message({message: "请输入促销描述",type: "warning"});
                return;
            }
            if(this.activityDetail.description==''){
                this.$message({message: "请输入活动描述",type: "warning"});
                return;
            }
            if(label==''){
                this.$message({message: "请输入标签名称",type: "warning"});
                return;
            }
            if(this.activityDetail.promotionChannel.length==0){
                this.$message({message: "请选择促销渠道",type: "warning"});
                return;
            }
            if(this.activityDate.length == 0){
                this.$message({message: "请选择活动时间",type: "warning"});
                return;
            }

            if(this.activityDetail.products.length == 0){
                this.$message({message: "请选择促销商品",type: "warning"});
                return;
            }

            for(var i=0;i<this.activityDetail.products.length;i++){
                var s = /(^[1-9](\d+)?(\.\d{1,2})?$)|(^\d\.\d{1,2}$)/ ;
                var r = /^\+?[1-9][0-9]*$/;　　//正整数
                var salePrice = this.activityDetail.products[i].price;
                var promotionPrice = this.activityDetail.products[i].promotionPrice;
                var itemLimit = this.activityDetail.products[i].limitQuantity;
                if(!s.test(promotionPrice)){
                    this.$message({message: "第"+ (i+1)+"行商品促销价输入有误，大于0且最多2位小数！",type: "warning"});
                    return;
                }
                if(parseFloat(salePrice)<parseFloat(promotionPrice)){
                    this.$message({message: "第"+ (i+1)+"行商品促销价输入有误，促销价不能大于销售价！",type: "warning"});
                    return;
                }
                var flag = r.test(itemLimit);
                if(!flag){
                    this.$message({message: "第"+ (i+1)+"行商品限购数量输入有误，请输入正整数！",type: "warning"});
                    return;
                }
            }

            var startDate = this.activityDate[0];
            var endDate = this.activityDate[1];
            if(typeof(this.activityDate[0])!="string"){
                startDate = this.activityDate[0].toLocaleDateString();
                endDate = this.activityDate[1].toLocaleDateString();
            }
            this.btnSaveLoadingA = true;
            var editRequest = {
                activityId:this.activityDetail.activityId,
                name:this.activityDetail.name,
                subtitle:this.activityDetail.subtitle,
                description:this.activityDetail.description,
                thumbUrl:this.activityDetail.thumbUrl,
                activityType:0,
                promotionType:this.activityDetail.promotionType,
                label:label,
                startTime:startDate,
                endTime:endDate,
                applyChannel:this.activityDetail.promotionChannel,
                activityProduct:this.activityDetail.products
            };
            this.btnSaveLoadingE = true;
            appSvc
            .editPromotion(editRequest)
            .then(
                res => {
                    var data = res.data;
                    this.$message({message: "修改成功！",type: "success"});
                    this.dialogVisible1 = false;
                    this.getPaged(false);
                },
                error => {
                    console.log(error);
                }
            )
            .catch(() => {})
            .finally(() => {
                this.btnSaveLoadingE = false;
            });
        },
        finishActivity(){
            this.$confirm('确定要结束活动吗?', '提示', {
            confirmButtonText: '确定',
            cancelButtonText: '取消',
            closeOnClickModal: false,
            type: 'warning'
            }).then(() => {
                var finishRequest = {
                    activityId:this.activityDetail.activityId
                };
                this.btnSaveLoadingF = true;
                appSvc
                .finishPromotion(finishRequest)
                .then(
                res => {
                    var data = res.data;
                    if(data){
                        this.$message({message: "操作成功",type: "success"});
                        this.dialogVisible1 = false;
                        this.getPaged(false);
                    }
                    else{
                        this.$message({message: "操作失败",type: "warning"});
                    }
                },
                error => {
                    console.log(error);
                }
                )
                .catch(() => {})
                .finally(() => {
                    this.btnSaveLoadingF = false;
                });
            }).catch(()=>{}); 
        },
        showProductDialog(){
            this.dialogVisible2 = true;
            this.productResult=[];
            this.searchPid1='';
        },
        queryProduct(){
            if(this.searchPid1==''){
                return;
            }
            this.tableLoading1 = true;
            var searchProductRequest = {
                searchContent:this.searchPid1
            };
            appSvc
            .searchProductByNameOrCode(searchProductRequest)
            .then(
                res => {
                    var data = res.data;
                    this.productResult = data;
                },
                error => {
                    console.log(error);
                }
            )
            .catch(() => {})
            .finally(() => {
                this.tableLoading1 = false;
            });
        },
        chooseProduct(objProduct){
            debugger;
            if(this.addOrDteailPage == 0){
                this.addRequest.thumbUrl = objProduct.icon;
                this.productData = [{
                    productCode:objProduct.productCode,
                    productName:objProduct.productName,
                    price:objProduct.salesPrice,
                    promotionPrice:'',
                    limitQuantity:''
                }];
            }
            else{
                this.activityDetail.thumbUrl = objProduct.icon;
                this.activityDetail.products = [{
                    productCode:objProduct.productCode,
                    productName:objProduct.productName,
                    price:objProduct.salesPrice,
                    promotionPrice:'',
                    limitQuantity:''
                }];
            }
            this.dialogVisible2 = false
        }
    }
}
</script>