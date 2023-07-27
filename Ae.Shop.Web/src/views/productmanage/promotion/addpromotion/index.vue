<template>
    <div class="container">
        <div class="customer-list-title">
            <span style="font-weight:bold;">促销活动配置</span>
            <el-input style="width:300px;margin-left:10px;" prefix-icon="el-icon-search" clearable size="small" placeholder="输入商品Pid，回车键搜索" v-model="searchPid" @keydown.enter.native="searchProduct(this);"></el-input>
        </div>
        <el-row>
            <el-col class="elCol">
                <el-row class="elRow">
                    <el-col class="titleSpan">活动名称：</el-col>
                    <el-col class="contentSpan">
                        <el-input clearable size="small" placeholder="输入活动名称" v-model="addRequest.name"></el-input>
                    </el-col>
                </el-row>
                <el-row class="elRow">
                    <el-col class="titleSpan">活动时间：</el-col>
                    <el-col class="contentSpan">
                        <el-date-picker style="width: 150px;"
                            v-model="addRequest.startTime"
                            type="date"
                            placeholder="开始日期">
                        </el-date-picker>
                        &nbsp;-&nbsp;
                        <el-date-picker style="width: 150px;"
                            v-model="addRequest.endTime"
                            type="date"
                            placeholder="终止日期">
                        </el-date-picker>
                    </el-col>
                </el-row>
                <el-row class="elRow">
                    <el-col class="titleSpan">促销渠道：</el-col>
                    <el-col class="contentSpan">
                        <el-checkbox-group v-model="checkedChannel" style="margin-top:5px;">
                            <el-checkbox v-for="channel in channelOptions" :label="channel.value" :key="channel.value">{{channel.name}}</el-checkbox>
                        </el-checkbox-group>
                    </el-col>
                </el-row>
            </el-col>
            <el-col class="elCol">
                <el-row class="elRow">
                    <el-col class="titleSpan">促销描述：</el-col>
                    <el-col class="contentSpan">
                        <el-input clearable size="small" placeholder="输入促销描述" v-model="addRequest.subtitle"></el-input>
                    </el-col>
                </el-row>
                <el-row class="elRow">
                    <el-col class="titleSpan">标签名称：</el-col>
                    <el-col class="contentSpan">
                        <el-radio style="margin-top:10px;" v-model="radio" @change="changeLabel()" label="1">默认(促销)</el-radio>
                        <el-radio style="margin-top:10px;" v-model="radio" @change="changeLabel()" label="2">自定义</el-radio>
                        <el-input :class="{'lableDisplay':display}" style="width:120px;margin-left:-20px;" clearable size="small" placeholder="自定义标签" v-model="addRequest.label"></el-input>
                    </el-col>
                </el-row>
                <el-row class="elRow">
                    <el-col class="titleSpan">活动类型：</el-col>
                    <el-col class="contentSpan">
                        <el-radio style="margin-top:10px;" v-model="radioPromotionType" label="0">自定义促销价</el-radio>
                    </el-col>
                </el-row>
            </el-col>
        </el-row>
        <el-row class="elRow">
            <el-col class="titleSpan">活动描述：</el-col>
            <el-col class="contentSpan">
                <Tinymce ref="content" v-model="addRequest.description" :height="350" />
            </el-col>
        </el-row>
        <div class="customer-list-title" style="margin-top: 15px !important;">
            <span style="font-weight:bold;">选择促销商品</span>
        </div>
        <el-table :data="productData" border style="width: 100%;margin-top: 20px">
            <el-table-column label="商品Pid" prop="productCode"></el-table-column>
            <el-table-column label="商品名称" prop="productName"></el-table-column>
            <el-table-column label="售价" prop="price"></el-table-column>
            <el-table-column label="促销价">
                <template slot-scope="scope">
                    <el-input clearable size="small" placeholder="输入促销价" v-model="scope.row.promotionPrice"></el-input>
                </template>
            </el-table-column>
            <el-table-column label="限购数量">
                <template slot-scope="scope">
                    <el-input clearable size="small" placeholder="输入限购数量" v-model="scope.row.limitQuantity"></el-input>
                </template>
            </el-table-column>
        </el-table>

        <div class="bodyBotton">
            <el-button type="primary" size="small" style="margin-left:40%;" icon="el-icon-check" @click="submitPromotion();">提交</el-button>
        </div>
    </div>
</template>

<script>
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";
import { appSvc } from "@/api/product/promotion";
import Tinymce from "@/components/Tinymce";
export default {
    components: { Tinymce },
    name:"addpromotion",
    data(){
        return{
            checkedChannel:[],
            shopId:[],
            channelOptions:[{
                value:'MiniApp',
                name:'微信小程序'
            },
            {
                value:'ShopApp',
                name:'门店管家'
            }],
            radio:"1",
            radioPromotionType:'0',
            display:true,
            searchPid:'',
            productData:[],
            addRequest:{
                name:'',
                subtitle:'',
                startTime:'',
                endTime:'',
                description:'',
                label:'',
                thumbUrl:''
            }
        }
    },
    methods:{
        changeLabel(){
            if(this.radio == 2){
                this.display=false;
            }
            else{
                this.display=true;
            }
        },
        //搜索商品
        searchProduct(o){
            if (event.keyCode == 13){
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
            }
        },
        //提交审核
        submitPromotion(){
            debugger;
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
            if(this.checkedChannel.length==0){
                this.$message({message: "请选择促销渠道",type: "warning"});
                return;
            }
            if(this.addRequest.startTime==''){
                this.$message({message: "请选择活动开始时间",type: "warning"});
                return;
            }
            if(this.addRequest.endTime==''){
                this.$message({message: "请选择活动结束时间",type: "warning"});
                return;
            }
            if(this.productData.length==0){
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
            var submitRequest = {
                name:this.addRequest.name,
                subtitle:this.addRequest.subtitle,
                description:this.addRequest.description,
                thumbUrl:this.addRequest.thumbUrl,
                activityType:0,
                promotionType:this.radioPromotionType,
                label:label,
                startTime:this.addRequest.startTime,
                endTime:this.addRequest.endTime,
                applyChannel:this.checkedChannel,
                shopIds:this.shopId,
                activityProduct:this.productData
            };
            appSvc
                .addPromotionActivity(submitRequest)
                .then(
                    res => {
                        var data = res.data;
                        this.$message({message: "提交成功！",type: "success"});
                        this.$router.push({ path: '/product/promotiondetail/'+data});
                        this.iniData();
                    },
                    error => {
                        console.log(error);
                    }
                )
                .catch(() => {})
                .finally(() => {});
        },
        iniData(){
            this.checkedChannel = [];
            this.shopId = [];
            this.radio = "1";
            this.radioPromotionType = '0';
            this.display = true;
            this.searchPid = '';
            this.productData = [];
            this.addRequest.name = '';
            this.addRequest.subtitle = '';
            this.addRequest.startTime = '';
            this.addRequest.endTime = '';
            this.addRequest.description = '';
            this.$refs.content.setContent('');
            this.addRequest.label = '';
            this.addRequest.thumbUrl = '';
        }
    }
}
</script>

<style lang="scss" scoped>
.container {
    margin-left: 30px;
    .titleSpan{
        width: 100px;
        text-align: right;
        font-weight: normal;
        margin-top: 10px;
    };
    .contentSpan{
        width: calc(100% - 100px);
        font-weight: normal;
    }
    .elCol{
        font-size: 13px !important;
        width:50%;
    };
    .elRow{
        margin-top:25px;
        font-size: 13px !important;
    };
    .customer-list-title{
      border-bottom: 1px solid #cfcfcf;
      padding: 10px;
      font-size: 13px !important;
      margin-top: 20px;
    };
    .bodyBotton {
        height: 60px;
        display: flex;
        align-items: center;
    }
}

/deep/ .el-input__inner{
    height: 32px;
}
/deep/ .el-input__icon{
    line-height: 32px;
}

.lableDisplay{
    display: none;
}
</style>