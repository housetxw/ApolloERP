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
                        placeholder="请输入姓名"
                        style="width:150px"
                        prefix-icon="el-icon-search"
                        v-model="condition.name">
                    </el-input>
                </el-form-item>
                <el-form-item>
                    <el-input class="input"
                        clearable
                        placeholder="请输入手机号"
                        style="width:150px"
                        prefix-icon="el-icon-search"
                        v-model="condition.phone">
                    </el-input>
                </el-form-item>
                <el-form-item>
                    <el-select
                        v-model="condition.provinceId"
                        clearable
                        filterable
                        placeholder="请选择省"
                        style="width:120px"
                        @change="getCity"
                        >
                        <el-option
                            v-for="item in provinceSel"
                            :key="item.regionId"
                            :label="item.name"
                            :value="item.regionId"
                        ></el-option>
                    </el-select>
                </el-form-item>
                <el-form-item>
                    <el-select
                        v-model="condition.cityId"
                        clearable
                        filterable
                        @change="getDistrict"
                        style="width:120px"
                        placeholder="请选择市"
                        >
                        <el-option
                            v-for="item in citySel"
                            :key="item.regionId"
                            :label="item.name"
                            :value="item.regionId"
                        ></el-option>
                    </el-select>
                </el-form-item>
                <el-form-item>
                    <el-select
                        v-model="condition.districtId"
                        clearable
                        filterable
                        style="width:120px"
                        placeholder="请选择区"
                        >
                        <el-option
                            v-for="item in districtSel"
                            :key="item.regionId"
                            :label="item.name"
                            :value="item.regionId"
                        ></el-option>
                    </el-select>
                </el-form-item>
            </template>

            <template v-slot:tb_cols>
                <el-table-column label="客户姓名" prop="name" :width="120"></el-table-column>
                <el-table-column label="手机号" prop="phone" :width="150"></el-table-column>
                <el-table-column label="邮箱" prop="email" :width="150"></el-table-column>
                <el-table-column label="地区" prop="shortAddress"></el-table-column>
                <el-table-column label="备注" prop="remark"></el-table-column>
                <el-table-column label="申请时间" prop="createTime" :width="130"></el-table-column>
            </template>

        </rg-page>
    </main>
</template>

<script>
import { Message, MessageBox } from "element-ui";
import { shopManageSvc } from "@/api/shopmanage/shopmanage";
export default {
    name:'joinlist',
    data(){
        return{
            tableLoading: false,
            currentPage: 1,
            totalList: 0,
            condition:{
                pageIndex: 1,
                pageSize: 20,
                name:'',
                phone:'',
                provinceId:'',
                cityId:'',
                districtId:''
            },
            tableData: [],
            provinceSel: [],
            citySel: [],
            districtSel: [],
            regionCondition: {
                regionId: undefined
            }
        }
    },
    created() {
        this.fetchData();
    },
    methods:{
        fetchData() {
            this.getProvince();
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
            shopManageSvc
            .getJoinInList(this.condition)
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
        getProvince(){
            this.regionCondition.regionId = 0;
            shopManageSvc
                .getRegionChinaListByRegionId(this.regionCondition)
                .then(
                res => {
                    this.provinceSel = res.data;
                },
                error => {
                    console.log(error);
                }
                )
                .catch(() => {});
        },
        getCity(){
            debugger;
            var provinceId = this.condition.provinceId;
            this.citySel = [];
            this.districtSel = [];
            this.condition.cityId = '';
            this.condition.districtId = '';
            if(provinceId!=''){
                this.regionCondition.regionId = provinceId;
                shopManageSvc
                .getRegionChinaListByRegionId(this.regionCondition)
                .then(
                    res => {
                    this.citySel = res.data;
                    },
                    error => {
                    console.log(error);
                    }
                )
                .catch(() => {});
            }

        },
        getDistrict(){
            debugger;
            var cityId = this.condition.cityId;
            this.districtSel = [];
            this.condition.districtId = '';
            if(cityId!=''){
                this.regionCondition.regionId = cityId;
                shopManageSvc
                .getRegionChinaListByRegionId(this.regionCondition)
                .then(
                    res => {
                    this.districtSel = res.data;
                    },
                    error => {
                    console.log(error);
                    }
                )
                .catch(() => {});
            }
        }
    }
}
</script>