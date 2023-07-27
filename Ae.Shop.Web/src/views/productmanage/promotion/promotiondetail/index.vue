<template>
    <div class="container">
        <div class="customer-list-title">
            <span style="font-weight:bold;">促销活动配置</span>
            <span style="color:red;font-weight:bold;margin-left:30%;">（状态：{{mainData.statusDisplay}}）</span>
        </div>
        <el-row>
            <el-col class="elCol">
                <el-row class="elRow">
                    <el-col class="titleSpan">活动名称：</el-col>
                    <el-col class="contentSpan">
                        <el-input clearable readonly size="small" placeholder="输入活动名称" v-model="mainData.name"></el-input>
                    </el-col>
                </el-row>
                <el-row class="elRow">
                    <el-col class="titleSpan">活动时间：</el-col>
                    <el-col class="contentSpan">
                        <el-date-picker style="width: 150px;"
                            v-model="mainData.startTime" readonly
                            type="date"
                            placeholder="开始日期">
                        </el-date-picker>
                        &nbsp;-&nbsp;
                        <el-date-picker style="width: 150px;"
                            v-model="mainData.endTime" readonly
                            type="date"
                            placeholder="终止日期">
                        </el-date-picker>
                    </el-col>
                </el-row>
                <el-row class="elRow">
                    <el-col class="titleSpan">促销渠道：</el-col>
                    <el-col class="contentSpan">
                        <el-checkbox-group v-model="checkedChannel" style="margin-top:5px;">
                            <el-checkbox disabled="" v-for="channel in channelOptions" :label="channel.value" :key="channel.value">{{channel.name}}</el-checkbox>
                        </el-checkbox-group>
                    </el-col>
                </el-row>
            </el-col>
            <el-col class="elCol">
                <el-row class="elRow">
                    <el-col class="titleSpan">促销描述：</el-col>
                    <el-col class="contentSpan">
                        <el-input clearable readonly size="small" placeholder="输入促销描述" v-model="mainData.subtitle"></el-input>
                    </el-col>
                </el-row>
                <el-row class="elRow">
                    <el-col class="titleSpan">标签名称：</el-col>
                    <el-col class="contentSpan" style="margin-top:10px;">
                        <span>{{mainData.label}}</span>
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
                <div style="border: 1px solid #cfcfcf;overflow-x:auto;" v-html="this.innerHtmlAttribute">
                </div>
                <!-- <Tinymce ref="content" v-model="mainData.description" :height="350" /> -->
            </el-col>
        </el-row>
        <div class="customer-list-title" style="margin-top: 15px !important;">
            <span style="font-weight:bold;">选择促销商品</span>
        </div>
        <el-table :data="mainData.products" border style="width: 100%;margin-top: 20px">
            <el-table-column label="商品Pid" prop="productCode"></el-table-column>
            <el-table-column label="商品名称" prop="productName"></el-table-column>
            <el-table-column label="售价" prop="price"></el-table-column>
            <el-table-column label="促销价" prop="promotionPrice"></el-table-column>
            <el-table-column label="限购数量" prop="limitQuantity"></el-table-column>
            <el-table-column label="已售数量" prop="soldQuantity"></el-table-column>
        </el-table>

        <div class="bodyBotton">
            <el-button type="danger" v-show="mainData.status == 3 || mainData.status == 4" size="small" style="margin-left:40%;" icon="el-icon-delete" @click="deletePromotion();">结束活动</el-button>
            <el-button type="primary" v-show="mainData.status == 1" size="small" icon="el-icon-check" style="margin-left:40%;" @click="auditPromotion();">审核通过</el-button>
        </div>
    </div>
</template>

<script>
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";
import { appSvc } from "@/api/product/promotion";
import Tinymce from "@/components/Tinymce";
export default {
    name:'promotiondetail',
    components: { Tinymce },
    data(){
        return{
            innerHtmlAttribute:'',
            detailRequest:{
                activityId:this.$route.params.activityId
            },
            checkedChannel:[],
            channelOptions:[{
                value:'MiniApp',
                name:'微信小程序'
            },
            {
                value:'ShopApp',
                name:'门店管家'
            }],
            radioPromotionType:'0',
            mainData:{
                activityId:'',
                name:'',
                subtitle:'',
                startTime:'',
                endTime:'',
                statusDisplay:'',
                status:0,
                label:'',
                promotionChannel:'',
                description:'',
                products:[]
            }
        }
    },
    methods:{
        getPromotionDetail(){
            appSvc
            .getPromotionDetail(this.detailRequest)
            .then(
            res => {
                var data = res.data;
                if(data != null){
                    this.mainData.activityId = data.activityId;
                    this.mainData.name = data.name;
                    this.mainData.subtitle = data.subtitle;
                    this.mainData.startTime = data.startTime;
                    this.mainData.endTime = data.endTime;
                    this.mainData.statusDisplay = data.statusDisplay;
                    this.mainData.status = data.status;
                    this.mainData.label = data.label;
                    this.mainData.promotionChannel = data.promotionChannel;
                    this.mainData.description = data.description;
                    this.mainData.products = data.products;
                    this.checkedChannel = data.promotionChannel.split(",");
                    this.innerHtmlAttribute = data.description;
                }
                else{
                    this.$message({message: "促销活动不存在",type: "warning"});
                }
            },
            error => {
                console.log(error);
            }
            )
            .catch(() => {})
            .finally(() => {});
        },
        deletePromotion(){
            this.$confirm('确定要结束活动吗?', '提示', {
            confirmButtonText: '确定',
            cancelButtonText: '取消',
            closeOnClickModal: false,
            type: 'warning'
            }).then(() => {
            appSvc
            .finishPromotion(this.detailRequest)
            .then(
            res => {
                var data = res.data;
                if(data){
                    this.$message({message: "操作成功",type: "success"});
                }
                else{
                    this.$message({message: "操作失败",type: "warning"});
                }
                this.getPromotionDetail();
            },
            error => {
                console.log(error);
            }
            )
            .catch(() => {})
            .finally(() => {
                this.tableLoading = false;
            });
            }).catch(()=>{}); 
        },
        auditPromotion(){
            appSvc
            .auditPromotionActivity(this.detailRequest)
            .then(
            res => {
                var data = res.data;
                if(data){
                    this.$message({message: "操作成功",type: "success"});
                }
                else{
                    this.$message({message: "操作失败",type: "warning"});
                }
                this.getPromotionDetail();
            },
            error => {
                console.log(error);
            }
            )
            .catch(() => {})
            .finally(() => {
                this.tableLoading = false;
            });
        }
    },
    created() {
        this.getPromotionDetail();
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