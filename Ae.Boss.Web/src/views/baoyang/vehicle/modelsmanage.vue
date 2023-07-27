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
</el-tabs>
</div>
<div class="buttons">
    <el-row>
        <el-button type="success" size="small" @click="getVehicleData();">查询</el-button>
        <el-button type="primary" size="small" @click="dialogVisible = true">添加数据</el-button>
        <el-checkbox style="float:right;font-weight:bold;margin-right:5px;margin-top:17px" v-model="checked" @change="showEdit()">修改数据</el-checkbox>
    </el-row>
</div>
<div id="vehicleData" style="margin-top: 18px; margin-bottom: 18px" class="table-responsive">
    <table class="tableContainer" v-html="this.innerHtmlData"></table>
</div>
<div id="loadingData" style="display: none; width: 100%; text-align: center; border: 1px solid #eee; padding: 8px 0 8px 0">
    <img src="../../../assets/bao_yang_images/loading.gif" style="height: 16px"/>
    <label style="margin-left: 5px;font-size: 75%;">正在光速加载中，请稍候！</label>
</div>
<div id="noData" style="display: none; width: 100%; text-align: center; border: 1px solid #eee; padding: 8px 0 8px 0">
    <span style="margin-left: 5px; font-weight: bolder;font-size: 75%;">无数据！</span>
</div>
<el-dialog title="添加车型信息" :close-on-click-modal='false' :visible.sync="dialogVisible" :width="dialogWidth">
    <form>
        <table>
            <tbody>
                <tr>
                    <td class="titleSpan">品牌:<span style="color: red">*</span></td>
                    <td class="contentSpan">
                        <el-select v-model="addVehicleRequest.brand" class="input" @change="getVehicleByBrandForAdd();" size="small">
                            <el-option
                            v-for="item in brandList"
                            :key="item.brand"
                            :label="item.brand"
                            :value="item.brand">
                            </el-option>
                        </el-select>
                    </td>
                    <td class="titleSpan">车系:<span style="color: red">*</span></td>
                    <td class="contentSpan">
                        <el-select v-model="vehicleIdAdd" @change="vehicleSelect" class="input" filterable placeholder="-请选择车系-" size="small">
                            <el-option
                            v-for="item in vehicleListForAdd"
                            :key="item.vehicleId"
                            :label="item.vehicle"
                            :value="item.vehicleId">
                            </el-option>
                        </el-select>
                    </td>
                    <td class="titleSpan">车系补充:<span style="color: red">*</span></td>
                    <td class="contentSpan">
                        <el-input class="input" size="small" placeholder="如:'A1-奥迪进口'=>'A1'" v-model="addVehicleRequest.vehicleSeries">
                        </el-input>
                    </td>
                </tr>
                <tr>
                    <td class="titleSpan">发动机型号:<span style="color: red">*</span></td>
                    <td class="contentSpan">
                        <el-input class="input" size="small" placeholder="如:N6A10XA3APSA" v-model="addVehicleRequest.engineNo">
                        </el-input>
                    </td>
                    <td class="titleSpan">排量:<span style="color: red">*</span></td>
                    <td class="contentSpan">
                        <el-input class="input" size="small" placeholder="如:2.0T" v-model="addVehicleRequest.paiLiang">
                        </el-input>
                    </td>
                    <td class="titleSpan">年款:<span style="color: red">*</span></td>
                    <td class="contentSpan">
                        <el-input class="input" size="small" placeholder="如:2015" v-model="addVehicleRequest.nian">
                        </el-input>
                    </td>
                </tr>
                <tr>
                    <td class="titleSpan">开始生产年份:<span style="color: red">*</span></td>
                    <td class="contentSpan">
                        <el-input class="input" size="small" placeholder="如:2015" v-model="addVehicleRequest.listedYear">
                        </el-input>
                    </td>
                    <td class="titleSpan">停产年份:</td>
                    <td class="contentSpan">
                        <el-input class="input" size="small" placeholder="如:2019" v-model="addVehicleRequest.stopProductionYear">
                        </el-input>
                    </td>
                    <td class="titleSpan">版型:<span style="color: red">*</span></td>
                    <td class="contentSpan">
                        <el-input class="input" size="small" placeholder="如:2.0T 无级 运动版" v-model="addVehicleRequest.salesName">
                        </el-input>
                    </td>
                </tr>
                <tr>
                    <td class="titleSpan">气缸数:<span style="color: red">*</span></td>
                    <td class="contentSpan">
                        <el-input class="input" size="small" placeholder="如:4" v-model="addVehicleRequest.cylinderNumber">
                        </el-input>
                    </td>
                    <td class="titleSpan">每缸气门数:<span style="color: red">*</span></td>
                    <td class="contentSpan">
                        <el-input class="input" size="small" placeholder="如:4" v-model="addVehicleRequest.valveNumPerCylinder">
                        </el-input>
                    </td>
                    <td class="titleSpan">燃油类型:<span style="color: red">*</span></td>
                    <td class="contentSpan">
                        <el-input class="input" size="small" placeholder="如:汽油" v-model="addVehicleRequest.fuelType">
                        </el-input>
                    </td>
                </tr>
                <tr>
                    <td class="titleSpan">指导价(万元):<span style="color: red">*</span></td>
                    <td class="contentSpan">
                        <el-input class="input" size="small" placeholder="如:20" v-model="addVehicleRequest.guidePrice">
                        </el-input>
                    </td>
                    <td class="titleSpan">功率(KW):<span style="color: red">*</span></td>
                    <td class="contentSpan">
                        <el-input class="input" size="small" placeholder="如:150" v-model="addVehicleRequest.powerKw">
                        </el-input>
                    </td>
                    <td class="titleSpan">状态:<span style="color: red">*</span></td>
                    <td class="contentSpan">
                        <el-select v-model="addVehicleRequest.status" class="input" filterable size="small">
                            <el-option
                            v-for="item in vehicleStatus"
                            :key="item.value"
                            :label="item.name"
                            :value="item.value">
                            </el-option>
                        </el-select>
                    </td>
                </tr>
                <tr>
                    <td class="titleSpan">变速箱类型:<span style="color: red">*</span></td>
                    <td class="contentSpan">
                        <el-select v-model="transmissionType" @change="transmissionTypeSelect" class="input" filterable size="small">
                            <el-option
                            v-for="item in transmissionTypeEnum"
                            :key="item.value"
                            :label="item.name"
                            :value="item.value">
                            </el-option>
                        </el-select>
                    </td>
                    <td class="titleSpan">变速箱型号:</td>
                    <td class="contentSpan">
                        <el-input class="input" size="small" placeholder="如:09G/TF-60SN" v-model="addVehicleRequest.transmission">
                        </el-input>
                    </td>
                    <td class="titleSpan">车身结构:</td>
                    <td class="contentSpan">
                        <el-input class="input" size="small" placeholder="如:5门5座两厢车" v-model="addVehicleRequest.carBody">
                        </el-input>
                    </td>
                </tr>
                <tr>
                    <td class="titleSpan">进气形式:</td>
                    <td class="contentSpan">
                        <el-select v-model="addVehicleRequest.airIntakeMode" filterable class="input" size="small">
                            <el-option
                            v-for="item in airIntakeModeEnum"
                            :key="item"
                            :label="item"
                            :value="item">
                            </el-option>
                        </el-select>
                    </td>
                    <td class="titleSpan">驱动方式:</td>
                    <td class="contentSpan">
                        <el-select v-model="addVehicleRequest.driveType" filterable class="input" size="small">
                            <el-option
                            v-for="item in driveTypeEnum"
                            :key="item"
                            :label="item"
                            :value="item">
                            </el-option>
                        </el-select>
                    </td>
                    <td class="titleSpan">底盘编号:</td>
                    <td class="contentSpan">
                        <el-input class="input" size="small" placeholder="如:F20" v-model="addVehicleRequest.chassisNo">
                        </el-input>
                    </td>
                </tr>
                <tr>
                    <td class="titleSpan">助力转向类型:</td>
                    <td class="contentSpan">
                        <el-select v-model="addVehicleRequest.steeringType" filterable class="input" size="small">
                            <el-option
                            v-for="item in steeringTypeEnum"
                            :key="item"
                            :label="item"
                            :value="item">
                            </el-option>
                        </el-select>
                    </td>
                    <td class="titleSpan">前制动器类型:</td>
                    <td class="contentSpan">
                        <el-select v-model="addVehicleRequest.frontBrakeType" filterable class="input" size="small">
                            <el-option
                            v-for="item in frontBrakeTypeEnum"
                            :key="item"
                            :label="item"
                            :value="item">
                            </el-option>
                        </el-select>
                    </td>
                    <td class="titleSpan">后制动器类型:</td>
                    <td class="contentSpan">
                        <el-select v-model="addVehicleRequest.backBrakeType" filterable class="input" size="small">
                            <el-option
                            v-for="item in backBrakeTypeEnum"
                            :key="item"
                            :label="item"
                            :value="item">
                            </el-option>
                        </el-select>
                    </td>
                </tr>
                <tr>
                    <td class="titleSpan">近光灯类型:</td>
                    <td class="contentSpan">
                        <el-select v-model="addVehicleRequest.dippedHeadlightType" filterable clearable class="input" placeholder="-请选择-" size="small">
                            <el-option
                            v-for="item in dippedHeadlightTypeEnum"
                            :key="item"
                            :label="item"
                            :value="item">
                            </el-option>
                        </el-select>
                    </td>
                    <td class="titleSpan">远光灯类型:</td>
                    <td class="contentSpan">
                        <el-select v-model="addVehicleRequest.highBeamType" filterable  clearable class="input" placeholder="-请选择-" size="small">
                            <el-option
                            v-for="item in highBeamTypeEnum"
                            :key="item"
                            :label="item"
                            :value="item">
                            </el-option>
                        </el-select>
                    </td>
                    <td class="titleSpan">燃油滤类型:</td>
                    <td class="contentSpan">
                        <el-select v-model="addVehicleRequest.fuelFilterType" filterable class="input" size="small">
                            <el-option
                            v-for="item in fuelFilterTypeEnum"
                            :key="item"
                            :label="item"
                            :value="item">
                            </el-option>
                        </el-select>
                    </td>
                </tr>
                <tr>
                    <td class="titleSpan">前胎规格:<span style="color: red">*</span></td>
                    <td class="contentSpan">
                        <el-input class="input" size="small" v-model="addVehicleRequest.frontTireSize">
                        </el-input>
                    </td>
                    <td class="titleSpan">后胎规格:<span style="color: red">*</span></td>
                    <td class="contentSpan">
                        <el-input class="input" size="small" v-model="addVehicleRequest.rearTireSize">
                        </el-input>
                    </td>
                    <td class="titleSpan">供油方式:</td>
                    <td class="contentSpan">
                        <el-select v-model="addVehicleRequest.oilSupplyMode" filterable class="input" size="small">
                            <el-option
                            v-for="item in oilSupplyModeEnum"
                            :key="item"
                            :label="item"
                            :value="item">
                            </el-option>
                        </el-select>
                    </td>
                </tr>
                <tr>
                    <td class="titleSpan">铝合金轮毂:</td>
                    <td class="contentSpan">
                        <el-select v-model="addVehicleRequest.alloyWheel" filterable class="input" size="small">
                            <el-option
                            v-for="item in alloyWheelEnum"
                            :key="item"
                            :label="item"
                            :value="item">
                            </el-option>
                        </el-select>
                    </td>
                    <td class="titleSpan">胎压监测装置:</td>
                    <td class="contentSpan">
                        <el-select v-model="addVehicleRequest.tireMonitorSystem" class="input" filterable size="small">
                            <el-option
                            v-for="item in tireMonitorSystemEnum"
                            :key="item"
                            :label="item"
                            :value="item">
                            </el-option>
                        </el-select>
                    </td>
                    <td class="titleSpan">发动机启停技术:</td>
                    <td class="contentSpan">
                        <el-select v-model="addVehicleRequest.autoStartStopSys" class="input" filterable size="small">
                            <el-option
                            v-for="item in autoStartStopSysEnum"
                            :key="item"
                            :label="item"
                            :value="item">
                            </el-option>
                        </el-select>
                    </td>
                </tr>
                <tr>
                    <td class="titleSpan">前悬架类型:</td>
                    <td class="contentSpan">
                        <el-select v-model="addVehicleRequest.frontSuspensionType" class="input" filterable clearable placeholder="-请选择-" size="small">
                            <el-option
                            v-for="item in frontSuspensionTypeEnum"
                            :key="item"
                            :label="item"
                            :value="item">
                            </el-option>
                        </el-select>
                    </td>
                    <td class="titleSpan">原配轮胎产品(PID):</td>
                    <td class="contentSpan">
                        <el-input class="input" size="small" v-model="addVehicleRequest.originalPid">
                        </el-input>
                    </td>
                    <td class="titleSpan">原配轮胎产品(无产品):</td>
                    <td class="contentSpan">
                        <el-input class="input" size="small" v-model="addVehicleRequest.originalName">
                        </el-input>
                    </td>
                </tr>
                <tr>
                    <td class="titleSpan">后悬架类型:</td>
                    <td class="contentSpan">
                        <el-select v-model="addVehicleRequest.rearSuspensionType" class="input" filterable clearable placeholder="-请选择-" size="small">
                            <el-option
                            v-for="item in rearSuspensionTypeEnum"
                            :key="item"
                            :label="item"
                            :value="item">
                            </el-option>
                        </el-select>
                    </td>
                </tr>
            </tbody>
        </table>
    </form>
    <span slot="footer" class="dialog-footer">
        <el-button style="float:left;" type="info" @click="resetData();">重 置</el-button>
        <el-button type="primary" @click="addBaoYangPackage();">保 存</el-button>
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
    name:'modelsmanage',
    data(){
        return{
            dialogWidth:"1130px",
            dialogVisible: false,
            brandList:[],
            vehicleList:[],
            vehicleListForAdd:[],
            paiLiangList:[],
            nianList:[],
            salesNameList:[],
            brand:'',
            vehicleId:'',
            vehicleIdAdd:'',
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
            vehicleDataList:[],
            transmissionType:'暂无',
            addVehicleRequest:{
                brand:'',
                vehicle:'',
                vehicleId:'',
                vehicleSeries:'',
                engineNo:'',
                paiLiang:'',
                nian:'',
                listedYear:'',
                stopProductionYear:'',
                salesName:'',
                cylinderNumber:'',
                valveNumPerCylinder:'',
                fuelType:'',
                guidePrice:'',
                powerKw:'',
                status:'New',
                transmissionTypeE:'',
                transmissionTypeC:'',
                transmission:'',
                carBody:'',
                airIntakeMode:'暂无',
                driveType:'暂无',
                chassisNo:'',
                steeringType:'暂无',
                frontBrakeType:'暂无',
                backBrakeType:'暂无',
                dippedHeadlightType:'',
                highBeamType:'',
                fuelFilterType:'暂无',
                frontTireSize:'',
                rearTireSize:'',
                oilSupplyMode:'暂无',
                alloyWheel:'无',
                tireMonitorSystem:'无',
                autoStartStopSys:'无',
                frontSuspensionType:'',
                rearSuspensionType:'',
                originalPid:'',
                originalName:''
            },
            vehicleStatus:[
                {
                    value:'New',
                    name:'已创建'
                },{
                    value:'Verified',
                    name:'已审核'
                },{
                    value:'Active',
                    name:'已上架'
                },{
                    value:'Deleted',
                    name:'已下架'
                }
            ],
            transmissionTypeEnum:[
                {
                    value:'2AT',name:'2档自动变速器'
                },
                {
                    value:'3AT',name:'3档自动变速器'
                },
                {
                    value:'4MT',name:'4档手动变速器'
                },
                {
                    value:'4A/MT',name:'4档手自一体变速器'
                },
                {
                    value:'4AT',name:'4档自动变速器'
                },
                {
                    value:'5MT',name:'5档手动变速器'
                },
                {
                    value:'5A/MT',name:'5档手自一体变速器'
                },
                {
                    value:'5DCT',name:'5档双离合变速器'
                },
                {
                    value:'5AT',name:'5档自动变速器'
                },
                {
                    value:'6MT',name:'6档手动变速器'
                },
                {
                    value:'6A/MT',name:'6档手自一体变速器'
                },
                {
                    value:'6DCT', name:'6档双离合变速器'
                },
                {
                    value:'6SMG',name:'6档序列式变速器'
                },
                {
                    value:'6AT',name:'6档自动变速器'
                },
                {
                    value:'7A/MT',name:'7档手自一体变速器'
                },
                {
                    value:'7DCT', name:'7档双离合变速器'
                },
                {
                    value:'7SMG',name:'7档序列式变速器'
                },
                {
                    value:'7AT',name:'7档自动变速器'
                },
                {
                    value:'8A/MT',name:'8档手自一体变速器'
                },
                {
                    value:'8DCT',name:'8档双离合变速器'
                },
                {
                    value:'8AT',name:'8档自动变速器'
                },
                {
                    value:'9A/MT',name:'9档手自一体变速器'
                },
                {
                    value:'9DCT', name:'9档双离合变速器'
                },
                {
                    value:'10A/MT',name:'10档手自一体变速器'
                },
                {
                    value:'AMT',name:'半自动变速器'
                },
                {
                    value:'电动车单速变速器',name:'电动车单速变速器'
                },
                
                {
                    value:'E-CVT',name:'电子无极变速器'
                },
                {
                    value:'E-CVT-AT',name:'电子无极变速器加自动变速箱'
                },
                {
                    value:'CVT',name:'无级变速器'
                },
                {
                    value:'暂无',name:'暂无'
                }
            ],
            airIntakeModeEnum:["自然吸气","机械增压","涡轮增压","机械+涡轮增压","双涡轮增压","暂无"],
            driveTypeEnum:["后置后驱","后置四驱","前置后驱","前置前驱","前置四驱","中置后驱","中置四驱","暂无"],
            steeringTypeEnum:["电动助力","电子电传助力","电子液压助力","机械液压助力","无助力","暂无"],
            frontBrakeTypeEnum:["盘式","鼓式","通风盘式","陶瓷通风盘式","暂无"],
            backBrakeTypeEnum:["盘式","鼓式","通风盘式","陶瓷通风盘式","暂无"],
            dippedHeadlightTypeEnum:["卤素","氙气","LED","激光"],
            highBeamTypeEnum:["卤素","氙气","LED","激光"],
            fuelFilterTypeEnum:["外置","内置","免维护","暂无"],
            oilSupplyModeEnum:["缸内直喷","多点电喷","混合喷射","暂无"],
            alloyWheelEnum:["有","无","选配"],
            tireMonitorSystemEnum:["有","无","选配"],
            autoStartStopSysEnum:["有","无","选配"],
            frontSuspensionTypeEnum:["麦弗逊独立悬架","三连杆悬架带硬轴螺旋弹簧","多连杆独立悬架","双横臂独立悬架带螺旋弹簧带横向稳定杆","空气悬架","整体桥非独立悬架带螺旋弹簧带横向稳定杆","EAS电子空气悬架","双横臂独立悬架","麦弗逊独立悬架带滑柱下控制臂带螺旋弹簧带横向稳定杆","双叉臂独立悬架","SLA两连杆空气悬架带稳定杆","SLA两连杆空气悬架带运动模","麦弗逊独立悬架带横向稳定杆","双横臂独立悬架带横向稳定杆","螺旋弹簧独立悬架","双摇臂独立悬架","双横臂独立悬架带扭杆弹簧","麦弗逊独立悬架带螺旋弹簧","多连杆双叉臂悬架带稳定杆","双叉臂独立悬架带横向稳定杆","双叉杆悬架带有铝制连杆","支柱独立悬架","滑柱独立悬架","独立悬架带横向摆臂带横向稳定杆","双叉臂独立悬架带双流路减震器带横向稳定杆","双叉臂独立悬架带减震器带横向稳定杆","麦弗逊独立悬架带双流路减震器带横向稳定杆","麦弗逊独立悬架支柱带螺旋弹簧带稳定杆","双横臂独立悬架带螺旋弹簧带稳定杆","双叉臂独立悬架带螺旋弹簧","长短臂独立悬架","双球节麦弗逊独立悬架","MRC主动电磁感应独立悬架","四轮独立悬架","独立悬架带特殊调教的螺旋弹簧带中空直接作用横向稳定杆","长短臂带高、低控制臂","麦弗逊独立悬架带A型横向连杆连接副车架带通过液压成型副车架与车体连接带双通道气压吸震筒带防倾杆","麦弗逊独立悬架带双通道气压吸震筒带防倾杆","增强型麦弗逊独立悬架","双叉杆独立悬架","双Y型悬架带稳定装置","多连杆独立悬架带阻尼可调减震器","麦弗逊独立悬架带下三角臂带推力带横向稳定杆","双叉独立悬架","双横臂纵置扭杆独立悬架","叉滑独立悬架","三连杆独立悬架","四连杆独立悬架","麦弗逊配三连杆悬架","不等长双横臂独立悬架","螺旋弹簧加杆系悬架","整体桥非独立悬架","整体前车桥带螺旋弹簧","螺旋弹簧","钢板弹簧","弹簧独立悬架带稳定杆","双横臂螺旋弹簧独立悬架","双叉杆独立悬架带螺旋弹簧","麦弗逊独立悬架带稳定杆","双横臂独立悬架带螺旋弹簧","双摆螺簧独立悬架","扭杆弹簧独立悬架","板簧非独立悬架","钢板弹簧带横向稳定杆","麦弗逊独立悬架带副车架","滑柱摆臂独立悬架","双叉骨独立悬架","双叉臂悬架带横向稳定杆","麦弗逊独立悬架带稳定杆的前束控制连杆","双A臂独立悬架","麦弗逊独立悬架带稳定杆(减震带预负荷机构和采用脉冲控制)","双叉独立悬架带横向稳定杆","叉骨与扭力杆独立悬架","双叉臂线圈弹簧仿倾杆","双A臂四连杆悬架带防倾杆","双横臂悬架带螺旋弹簧","横置单簧独立悬架","横臂扭杆弹簧独立悬架","麦弗逊独立悬架带平衡杆","高位斜置双Y型摆臂独立悬架","麦弗逊独立悬架带螺旋弹簧带横向稳定杆带双筒充气减震器","双球节弹簧减震支柱前桥","双球节控制臂悬架系统","双球节弹簧减振支柱前桥","单节点弹簧减震支柱前桥","双横臂悬架","变截面板簧非独立悬架","双弹簧扭杆弹簧独立悬架","双叉独立悬架带扭杆弹簧带稳定杆","双叉形臂独立悬架带扭杆弹簧","螺旋簧整体非独立悬架前桥","麦弗逊独立悬架带组合梯形副车架","滑柱单摆臂独立悬架带螺旋弹簧","双摆臂独立悬架带螺旋弹簧","不等长双横臂扭杆下置独立悬架","三杆双横臂螺旋弹簧独立悬架","麦弗逊独立悬架带稳定杆(采用脉冲控制减震器)","横臂扭杆下置独立悬架","双摆臂独立悬架","纵置扭杆弹簧独立悬架","扭杆弹簧悬架","不等长双横臂扭杆弹簧独立悬架","不等长双A臂独立悬架带扭杆弹簧带充气双向作用液压筒减震器","麦弗逊独立悬架带圆柱螺旋弹簧带双向作用筒减震器","麦弗逊独立悬架带螺旋弹簧带稳定杆","双叉臂独立悬架带阻尼可调减震器","扭杆弹簧独立悬架带双向稳定杆","可摆动的滑柱独立悬架","麦弗逊独立悬架带液压铸造副车架带防倾杆","超路感高循迹麦弗逊独立悬架","L型摆臂独立悬架","HiPer-Strut高性能悬架","HiPer-Strut双独立悬架","加强型麦弗逊独立悬架","左右长短臂","Hiper-Strut","左右长短臂附防倾杆独立悬架","麦弗逊独立悬架带三角型下横臂及横向稳定杆带螺旋弹簧","麦弗逊独立悬架带三角型下横臂及横向稳定杆带充气液压减震器","麦弗逊独立悬架带三角型下横臂及横向稳定杆带充气减震液压器带螺旋弹簧","轻质五连杆独立悬架","五连杆独立悬架","轻质四连杆独立悬架","优化麦弗逊独立悬架","双叉杆悬架","双叉臂独立悬架带空气弹簧","麦弗逊滑柱独立悬架","麦弗逊独立悬架带副车架带防倾杆","麦弗逊独立悬架带防侧倾稳定杆","L臂麦弗逊支柱独立悬架","整体独立悬架","双横臂扭杆独立悬架","双叉臂扭杆弹簧独立悬架","扭连杆弹簧结构","独立双横臂","纵置钢板弹簧非独立悬架","麦弗逊独立悬架+横向稳定杆","前双摆臂独立悬架","螺旋弹簧双叉臂独立悬架","AIRMATIC主动空气悬架","双叉臂独立前悬架带横向稳定杆","独立双叉悬架带横向稳定杆","轻质四连杆独立悬","双横臂扭杆弹簧独立悬架","双球节麦弗逊悬架","双叉臂独立悬架带电磁减振","独立双叉悬架+横向稳定杆","支柱独立悬架带平衡杆","双横臂螺旋独立悬架","双叉骨悬架","麦弗逊独立悬架带螺旋弹簧带双管减震器带稳定杆(运动版为运动型调校减震器及弹簧)","麦弗逊独立悬架带运动调校減震器带弹簧","双横臂螺旋弹簧带横向稳定杆","带横向稳定杆的麦弗逊独立悬架","双叉臂悬架+横向稳定杆带AVS适应可调悬架","螺旋弹簧麦弗逊独立悬架","钢板弹簧非独立悬架","五连杆整体桥非独立悬架带前稳定杆","双横臂纵置扭杆","双叉臂螺旋独立悬架","双球节麦弗逊前悬架","麦弗逊独立悬架带电磁减振","独立双叉臂","长短臂前悬架","双摆臂扭杆弹簧","自适应空气悬架","双横臂悬架带转向","麦弗逊独立悬架带电磁减震","独立双叉臂悬架+横向稳定杆","双横臂+螺旋弹簧独立悬架","前桥独立悬架","AIRMATIC空气悬架","MBC智能车身控制及ABC主动车身控制的空气悬架","双横臂螺旋弹簧","带横向稳定杆麦弗逊独立悬架","双横臂独立悬架带扭杆带横向稳定杆","独立双V形架带采用防下扎几何、螺旋弹簧、防侧倾杆带单管自适应阻尼器","双叉臂独立悬架带防倾杆带可手动或自动控制的电子避震器","双横臂独立悬架铝制","轻质四连杆独立悬架(空气悬架)","麦弗逊独立悬架带三角形下控制臂带铝制副车架","麦弗逊独立悬架带奥迪电磁减震","双接头弹簧减震支柱悬架带主销前倾后置","轻质双球节弹簧减震支柱前桥","铝合金双接头弹簧柱","Porsche优化的麦弗逊独立悬架","轻质弹簧滑柱悬架","大型双摇臂独立悬架","麦弗逊独立悬架带圈弹簧","AMG运动型悬架","AIRMATIC空气悬架带ADS自动吸震系统及平衡控制","AIRMATIC空气悬架带有自适应减震系统和全方位自调节水平高度","铝制双叉臂结构的AMG运动型悬架","四轮双叉臂独立悬架","双叉臂空气悬架带抗侧倾杆带高度可调","自动空气动力悬架带电子减震","三叉臂悬架","双摇臂悬架带螺旋弹簧","高低控制臂带卷簧带减震器带防倾杆","双横臂独立悬架带扭杆","双横向导臂加减震支柱","多叉骨","双叉骨独立悬架带防侧倾杆","麦花臣滑柱(连L形下横臂)悬架","麦弗逊独立滑柱悬架带横向稳定杆","双叉臂独立悬架带横向稳定杆带AVS适应可调","双叉臂独立悬架带螺旋弹簧带气压减震器带防侧倾杆","双球节多连杆空气悬架带稳定杆","滑柱(运动型)悬架","麦弗逊独立悬架带L下控制臂带独立副车架带稳定杆","被动叉型悬架","独立悬架带稳定杆带弹簧","长短臂独立悬架带稳定杆螺旋弹簧带充气减震器","麦弗逊独立悬架带螺旋弹簧带充气减震器带横向稳定杆","五连杆独立悬架整体桥带气动减震器带稳定杆","螺旋弹簧整体桥","短长臂独立悬架","双叉臂董雷悬架带防倾杆带防止下潜带防止下沉","铝合金双A臂悬架带防倾杆带防潜带防沉","铝合金双A臂悬架带防倾杆","双叉臂独立悬架带电磁减震","横向推杆水平悬架","双叉臂螺旋弹簧独立悬架"],
            rearSuspensionTypeEnum:["后悬架类型","多连杆独立悬架","H型整体后桥","扭力梁悬架","纵臂扭转梁悬架带横向稳定杆","整体桥非独立悬架","三连杆悬架带硬轴螺旋弹簧","麦弗逊独立悬架","扭力梁非独立悬架","双阻尼螺旋弹簧","空气悬架","电子控制高度可调空气悬架带横向稳定杆","EAS电子空气悬架","双横臂独立悬架","独立悬架","连杆支柱独立悬架","麦弗逊独立悬架带滑柱带梯形连杆带螺旋弹簧","空气悬架臂","多连杆空气悬架带稳定杆","多连杆空气悬架带运动模","双叉臂独立悬架带空气弹簧","多连杆独立悬架带横向稳定杆","多连杆悬架","双叉臂独立悬架","五连杆独立悬架","螺旋弹簧独立悬架","双摇臂独立悬架","多连杆悬架带螺旋弹簧带稳定杆","整体扭杆梁非独立悬架","纵臂扭转梁半独立悬架","多连杆非独立悬架","多连杆独立悬架带稳定杆","铝合金多连杆悬架","多连杆独立悬架带铝制连杆","多连杆独立悬架带螺旋弹簧","多连杆独立悬架带双流路减震器带横向稳定杆","多连杆独立悬架带减震器带横向稳定杆","双连杆独立悬架","双叉臂独立悬架带自动水平调整功能横向稳定杆","多连杆独立悬架带螺旋弹簧带稳定杆","拖拽臂(纵臂扭转梁)悬架","半纵臂悬架带螺旋弹簧带稳定杆","半拖曳臂悬架带螺旋弹簧带稳定杆","三连杆螺旋弹簧整体桥","五连杆非独立悬架","五连杆悬架带螺旋弹簧","扭力梁后悬架","双叉横臂独立悬架","MRC主动电磁感应独立悬架","四轮独立悬架","五连杆MRC主动电磁感应悬架系统","H型摆臂带中空横向稳定杆","多连杆悬架带横向稳定杆","四连杆独立悬架","增强型四连杆悬架","复合扭力梁悬架","扭力梁半独立悬架","拖曳臂悬架","多连杆悬架带稳定装置带平衡杆","多连杆独立悬架带阻尼可调减震器","纵臂扭转梁非独立悬架","连杆独立悬架带螺旋弹簧带稳定杆","纵向摆臂独立悬架带横向稳定杆","四连杆机构悬架","钢板弹簧","五连杆钢板弹簧","双向作用液压筒减震器悬架","整体前车桥带钢板弹簧","H型扭力梁悬架","五连杆悬架","五连杆螺旋弹簧非独立悬架","钢板弹簧非独立悬架","纵置钢板弹簧","五连杆螺旋弹簧","双叉杆独立悬架带螺旋弹簧","双连杆独立悬架带稳定杆","扭转梁悬架","五连杆非独立悬架带螺旋弹簧","四连杆机构悬架带螺簧","变刚度钢板弹簧","板簧非独立悬架","纵向托臂扭力梁悬架","拖曳臂扭力梁悬架","五连杆双横臂独立悬架","拖曳臂扭力杆悬架","纵臂扭力梁半独立悬架","纵臂扭力梁非独立悬架","麦弗逊配三连杆独立悬架","螺旋弹簧非独立悬架","螺旋弹簧","单纵摆臂非独立悬架","单摆纵臂非独立悬架","纵向单摆臂非独立悬架","拖臂悬架带双向作用筒","双叉骨独立悬架","双横臂独立悬架带稳定杆的反作用连","双纵臂螺旋弹簧","霍奇基斯悬架","拖曳臂非独立悬架","扭力梁悬架带稳定杆","扭力梁悬架带稳定杆(减震带预负荷机构和采用脉冲控制)","双扭杆纵控制臂独立悬架带横向稳定杆","纵向摆臂独立悬架","带稳定杆扭力梁后悬架","复合多连杆独立悬架带横向稳定杆","梯形控制臂独立悬架","整体复合悬架","整体扭杆梁半独立悬架","车轴叶片弹簧","轴叶片弹簧","五片钢板弹簧非独立悬架","半拖曳臂独立悬架","五连杆独立悬架带横向稳定杆","纵置板簧非独立悬架","半拖曳独立悬架","拖曳臂独立悬架带螺旋弹簧","拖曳臂独立悬架","拖曳悬架","单斜臂螺簧独立悬架","纵臂扭转梁独立悬架","纵臂扭杆独立悬架","纵向变截面钢板带弹簧非独立悬架","H型扭力梁半独立悬架","H型扭力梁非独立悬架","多连杆独立悬架带垂向控制臂","多连杆独立悬架带垂直控制臂","封闭截面扭力梁半独立悬架","扭力梁拖曳臂悬架","五连杆独立悬架带螺旋弹簧","多连杆独立悬架带横向稳定杆带螺旋弹簧带双筒充气减震器","三连杆非独立悬架","三连杆整体桥悬架带螺旋弹簧","中央控制臂后桥","螺旋簧前置斜定位独立悬架","半拖独立悬架","纵置板簧","双横臂独立悬架带扭杆弹簧","拖曳臂半独立悬架带弹性扭力梁","螺旋簧前置斜定位单臂独立悬架","螺旋前置斜定位单臂独立悬架","独立后桥弹簧避震悬架","扭转梁半独立悬架","扭转梁非独立悬架","后扭力梁拖曳臂悬架","拖曳臂后悬架","复合扭转梁非独立悬架","H型纵向摆臂悬架","后H型纵向摆臂","扭转梁独立悬架","螺旋弹簧减震带稳定杆带双向减震器","多连杆独立悬架带拖臂簧","整体桥悬架带螺旋弹簧带稳定杆","整体桥悬架带螺旋弹簧","整体桥变刚度钢板弹簧悬架","螺旋簧整体后桥","多连杆独立悬架带副车架","滑柱单摆臂独立悬架","纵向摆臂带减震器独立悬架","纵臂扭梁非独立悬架","扭梁螺旋弹簧非独立","纵向摆臂抗扭梁复合悬架","扭梁半独立悬架带螺旋弹簧","E型多连杆独立悬架带梯形副车架","扭梁非独立悬架带螺旋弹簧","双摆臂独立悬架","纵臂悬架","滑柱双摆臂独立悬架带螺旋弹簧","纵臂扭梁半独立悬架","纵向摆臂抗扭复合悬架","拖曳非独立悬架带螺旋弹簧","扭梁半独立悬架","不等长双横臂扭杆下置独立悬架","少片变刚度钢板弹簧","独立随动悬架","空间四杆螺旋弹簧独立悬架","双连杆独立悬架带稳定杆(采用脉冲控制减震器)","E型多连杆独立悬架","双叉臂悬架带双向充气减震器带螺旋弹簧","钢板弹簧悬架","摆臂非独立悬架","变刚度叶片弹簧","麦弗逊独立悬架带横向稳定杆","复合纵臂半独立悬架","拖曳臂半独立悬架","三连杆拖曳臂独立悬架","纵置变截面少片簧带双向作用液压筒减震器","纵置变截面少片簧","可变刚度螺旋弹簧悬架带四连杆","四连杆悬架带螺旋弹簧带液压筒减震器","不等长可变刚度螺旋弹簧悬架带四连杆带充气双向作用液压筒减震器","可变刚度螺旋弹簧悬架带四连杆带充气双向作用液压筒减震器","扭转梁随动臂半独立悬架","非独立车轴悬架带螺旋弹簧","横置钢板独立悬架","五片钢板弹簧非独立悬架带双向作用筒减震器","纵向拖曳臂非独立悬架","复合多连杆独立悬架","纵向拖臂多连杆悬架","纵向拖曳臂半独立悬架","扭转梁半独立悬架带圆柱螺旋弹簧带双向作用筒减震器","纵向拖曳臂悬架","拖曳臂独立悬架带双连杆","双连杆独立悬架带横向稳定杆","拖曳臂半独立悬架带横向拉杆","椭圆钢板弹簧带双向减震","半独立非驱动桥","复合扭转梁半独立悬架","纵向摆臂非驱动桥","抗扭非独立悬架","复合扭转梁独立悬架","抗扭整体独立悬架","抗扭整体非独立悬架","纵向横臂扭杆梁复合半独立悬架","加强桥非独立悬架","精准转H型扭转梁","Z型多连杆独立悬架带副车架带防倾杆","独创性Z型纵摆臂双横臂独立悬架","变截面钢板弹簧","H型扭力梁非独立","多连杆Z型悬架","Z型纵摆臂独立悬架","管状U型扭杆梁","复合扭转梁悬架","复合扭力梁非独立悬架","三连杆独立悬架","增强型多连杆独立悬架","增强型H-arm高等级悬架","斜置单臂独立悬架","扭杆梁+瓦特连杆非独立悬架","增强型复合扭杆梁悬架带瓦特连杆","增强型复合扭杆梁悬架","加强型复合扭杆梁悬架","H-arm四连杆独立悬架","多连杆四轮独立悬架","扭杆梁瓦特连杆非独立悬架","扭杆梁后桥半独立悬架","加强型后车桥拖曳臂悬架","变刚度非对称悬架带钢板弹簧","复合曲柄","连杆摆臂半独立悬架带螺旋弹簧","五连杆摆臂悬架","五连杆半独立悬架","五连杆摆臂半独立悬架","半独立悬架","螺旋弹簧半独立悬架","五连杆半独立悬架带螺旋弹簧","单片变截面刚板弹簧","可变形横梁准独立悬架","纵向摆臂独立悬架带横向稳定杆带扭杆弹簧","可变形横梁半独立悬架","可变形横梁带横向稳定杆","可变形横梁","可变形横梁带横向稳定杆带垂直布置减震器","扭力杆悬架带螺旋弹簧带减震筒","减震韧性多连杆独立悬架","扭转梁随动臂独立悬架","单纵臂整体后桥带螺旋弹簧","复合扭梁半独立悬架","双连杆麦弗逊滑柱独立悬架带横向稳定杆","滑柱双摆臂独立悬架带横向稳定杆带纵拉力杆","双横臂独立悬架带横向稳定杆","滑柱双横臂独立悬架带螺旋弹簧带横向稳定杆","纵臂扭力梁悬架带横向稳定杆","双A臂独立悬架","梯形导向摆臂独立悬架","梯形连杆独立悬架","优化的四连杆独立悬架","复合扭转梁半独立悬架带充气避震系统","复合扭力梁半独立悬架","加强四连杆独立悬架","加强型非独立悬架","升级柔性后轴多连杆独立悬架","创新耦合杆悬架","双叉杆悬架","新型拖曳臂悬架(减震器带减震簧分离)","四连杆非独立悬架","四连杆整体桥非独立悬架","H型扭力梁独立悬架带稳定杆","H型扭力梁悬架带横向稳定杆","TTL双天梯多连杆独立悬架","纵置半椭圆钢板弹簧","多连杆悬架带螺旋弹簧","E型多连杆独立悬架带横向稳定杆","H型多连杆独立悬架","纵置单臂非独立悬架","SLA带Control带Blade带全独立悬架","SLA带Control带Blade带全独立悬架带副车架带防倾杆","复合连杆独立悬架","刀锋全独立多连杆悬架带防倾杆","螺旋杆悬架","拖臂半独立悬架","拖曳臂非独立悬架带螺旋弹簧","非独立车距螺旋弹簧","五片钢板弹簧","多连杆非独立悬架带螺旋弹簧","整体独立悬架","纵臂扭转梁复合悬架","四连杆非独立悬架带螺旋弹簧","单纵臂独立悬架","四连杆重载螺旋弹簧","钢板四连杆重载螺旋弹簧","渐变刚度非对称钢板弹簧","单片变截面钢板弹簧","纵摆臂悬架","独立双横臂","纵置钢板弹簧非独立悬架","带横向稳定杆+复合多连杆独立悬架","五连杆整体桥非独立悬架带带稳定杆","后扭杆梁+瓦特连杆非独立悬架","纵臂扭力梁带横向稳定杆","非独立单纵摆臂","多连杆独立悬架+横向稳定杆","可变形横梁带带横向稳定杆","独立双连杆麦弗逊滑柱带横向稳定杆","梯形连杆后悬架","AIRMATIC主动空气悬架","H形扭力梁非独立悬架","扭梁非独立悬架","五连杆后悬架","纵向摆臂+抗扭梁复合悬架","多连杆整体桥悬架带瓦特连杆及稳定杆","双连杆独立悬架+横向稳定杆","双叉臂独立悬架带电磁减振","四连杆空气悬架","多连杆独立悬架带平衡杆","拖臂非独立悬架","钢板弹簧结构","整体扭杆梁非独立","纵臂扭力梁复合悬架","后扭力梁带螺旋弹簧及双管减震器(运动版为运动型调校减震器及弹簧)","扭力梁悬架带运动调校减震器带弹簧","双横臂螺旋弹簧带横向稳定杆","双横臂独立悬架带螺旋弹簧","后螺旋弹簧非独立悬架","可变形横梁准独立后悬架","带横向稳定杆的复合多连杆独立悬架","纵向摆臂抗扭梁复合后悬架","螺旋弹簧扭转梁","梯形控制臂连杆独立悬架","纵臂扭转力非独立悬架","多连杆悬架+横向稳定杆带AVS适应可调悬架","螺旋弹簧双叉臂独立悬架","两连杆独立悬架","RAS后空气悬架","后导向杆非独立悬架","拖曳臂带扭粱非独立悬架","扭力梁螺旋弹簧非独立悬架","支柱多连杆独立悬架","双叉臂悬架带横向稳定杆","拖曳臂附扭力杆悬架","半拖曳臂独立","铝合金多连杆","四连杆螺旋弹簧悬架液压筒减振器","五连杆+钢板弹簧","五连杆","四连杆独立悬架带电磁减振","变截面扭力梁非独立悬架","多连杆铝制整体后桥","五连杆摆臂","双叉臂悬架","钢板弹簧整体桥","板簧纵置非独立悬架","拖臂簧+多连杆独立悬架","拖曳臂非独立悬架带横向拉杆","多连杆螺旋弹簧独立","自适应空气悬架","双横臂独立悬架带上控制臂带横拉杆","四连杆独立悬架带电磁减震","拖曳臂半立悬架","独立双叉悬架带横向稳定杆","纵臂附横向推力杆非独立后悬架","扭力梁非独立悬架带空气减震","五连杆整体桥非独立悬架带前稳定杆","扭力梁后悬架+后瓦特连杆","单臂后独立悬架","多片钢板弹簧悬架","后桥单片变截面弹簧悬架","多连杆整体铝制后桥悬架","AIRMATIC空气悬架","MBC智能车身控制及ABC主动车身控制的空气悬架","双横臂带横向稳定杆","四连杆悬架带横向稳定杆带螺旋弹簧","单臂独立悬架","五连杆螺旋簧","多连杆独立后悬架带横向稳定杆","双叉骨悬架","双V形独立悬架带防后坐和防抬升几何带螺旋弹簧带防侧倾杆带单管自适应阻尼器","双叉臂独立悬架带防倾杆及可手动或自动控制的电子避震器","双叉臂独立悬架带螺旋弹簧","双横臂独立悬架铝制","梯形连杆独立悬架(空气悬架)","四连杆独立悬架带弹簧带减震器分开布置带副车架","纵向双叉臂带辅助控制臂","四连杆独立悬架带奥迪电磁减震","中心臂车桥带有纵向摆臂带双横向摆臂","轻质钢结构五向后轴","五联臂后悬桥","整体铝制后桥","双横拉杆和减震支柱带防侧倾杆","空气悬架带自动调节车身水平","Porsche优化的麦弗逊独立悬架","轻质弹簧滑柱悬架","多控制臂独立悬架带卷圈弹簧","大型双摇臂独立悬架","AMG运动型悬架","AIRMATIC空气悬架带ADS自动吸震系统及平衡控制","AIRMATIC空气悬架带有自适应减震系统及全方位自调节水平高度","铝制双叉臂结构的AMG运动型悬架","扭力梁独立悬架","整体的多连杆悬架","梯形多连杆空气悬架带抗侧倾杆及高度可调","四连杆悬架重载螺旋弹簧","自动空气动力悬架带电子减震","双摇臂独立悬架带螺旋弹簧","高低控制臂带卷簧带减震器带防倾杆","双横向导臂加减震支柱","多连杆悬架带防侧倾杆","双叉臂带横向稳定杆","叶片弹簧","四连杆悬架","四连杆悬架带螺旋弹簧","双叉臂独立(连纵臂)悬架","双叉臂独立悬架带横向稳定杆","麦弗逊独立滑柱双连杆悬架带横向稳定杆","麦弗逊滑柱独立双连杆悬架带横向稳定杆","多连杆悬架带横向稳定杆带AVS适应可调","双叉臂独立悬架带螺旋弹簧和气压减震器及防侧倾杆","四连杆空气悬架带稳定杆","四连杆空气弹簧独立悬架","双球节多连杆空气悬架带稳定杆","四连杆悬架带横向稳定杆","双叉臂(运动型)悬架","多连杆独立悬架带拖曳控制臂及独立副车架带稳定杆","螺旋弹簧独立悬架带稳定杆","双叉臂独立悬架带空气弹簧及车身高度可调","双叉臂独立悬架带空气弹簧带车身高度可调","五连杆悬架整体桥带稳定杆螺旋弹簧及充气减震器","多连杆独立悬架带螺旋弹簧带充气减震器带横向稳定杆","五连杆独立悬架整体桥带气动减震器带稳定杆","五连杆整体桥带气动减震器带稳定杆","螺旋弹簧整体桥","双叉臂独立悬架带防倾杆带防止下潜带防止下沉","铝合金双A臂悬架带防倾杆带防潜带防沉","铝合金双A臂悬架带防倾杆","上下连杆","双叉臂独立悬架带电磁减震","横向推杆水平悬架","四连杆独立悬架带空气弹簧"]
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
        getVehicleByBrandForAdd(){
            if(this.addVehicleRequest.brand==''){
                return;
            }
            this.vehicleId='';
            this.paiLiang='';
            this.nian='';
            this.tid='';
            var vehicleCondition = {brand:this.addVehicleRequest.brand};
            appSvc.getListByBrand(vehicleCondition).then(res=>{
                var data = res.data;
                this.vehicleListForAdd = data;
            },error=>{
                console.log(error)
            })
            .catch(()=>{})
            .finally(()=>{})
        },
        vehicleSelect(selVal){
            this.addVehicleRequest.vehicle = selVal.vehicle;
            this.addVehicleRequest.vehicleId = selVal.vehicleId;
        },
        transmissionTypeSelect(selVal){
            this.addVehicleRequest.transmissionTypeE = selVal.value;
            this.addVehicleRequest.transmissionTypeC = selVal.name;
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
                vehicleId:'',
                paiLiang:'',
                nian:'',
                tid:''};
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
            this.showLoading();
            console.log("condition: " + JSON.stringify(vehicleRequest));
            appSvc.getVehicleInfoList(vehicleRequest).then(res=>{
                var data = res.data;
                if(data!=null && data.length>0){
                    document.getElementById("vehicleData").style.display = "";
                    this.vehicleDataList = data;
                    this.bindData(data);
                }
                else{
                    document.getElementById("noData").style.display = "";
                }
            },error=>{
                console.log(error)
            })
            .catch(()=>{})
            .finally(()=>{
                this.hideLoading();
            })
        },
        bindData(data){
            var timingVehicleAccessoryUpdate = "<select onchange='TimingVehicleComponentUpdate(this)' onblur = 'hideEidArea(this)' style='display: none'></select>";
            var statusDropDownListEdit = "<li onclick='showStatusDropDownListArea(this)'><span class='ui-icon ui-icon-pencil'></span></li>";
            var operationEditVehicle = "<li onclick='EditVehicle(this)'><span class='ui-icon ui-icon-pencil'></span></li>";
            var operationTidDelete = "<li class='ui-state-error' onclick='showDeleteTidConfirm(this)'><span class='ui-icon ui-icon-trash'></span></li>";
            var vehiclePartupdate = "<input type='text' style='display: none' onkeydown='vehicleItemupdate(this)' onblur = 'hideEidArea(this)'/>";
            var operationEdit = "<li onclick='showEditArea(this)'><span class='ui-icon ui-icon-pencil'></span></li>";
            var vehicleInfoInput = "<input type='text' style='display: none' onkeydown='submitEditVehicleInfo(this)' onblur = 'hideEidArea(this)'/>";
            var vehicleAccessoryUpdate = "<select onchange='VehicleComponentUpdate(this)' onblur = 'hideEidArea(this)' style='display: none'></select>";
            var dropDownListEdit = "<li onclick='showDropDownListArea(this)'><span class='ui-icon ui-icon-pencil'></span></li>";
            var editVehicleTires = "<li onclick='UpdateVehicleTire(this,0)'><span class='ui-icon ui-icon-pencil'></span></li>";
            var editTiresMatch = "<li onclick='UpdateTiresMatch(this)'><span class='ui-icon ui-icon-pencil'></span></li>";
            var editStyle = "<ul class='operation-group' style='"+this.showOperation()+"'>";
            var html = "";
            var rowTidId = "<tr><th>TID</th>";
            var rowVehicleID = "<tr><td style='font-weight:bolder'>VehicleID</td>";
            var rowStatus = "<tr><td style='font-weight:bolder'>状态</td>"
            var brandFromVehicleType = "<tr><td style='font-weight:bolder'>品牌</td>";
            var vehicleFromVehicleType = "<tr><td style='font-weight:bolder'>车系</td>";
            var vehicleType = "<tr><td style='font-weight:bolder'>车系补充</td>";
            var rowVehicleName = "<tr><td style='font-weight:bolder'>车型名称</td>";
            var rowEngineNo = "<tr><td style='font-weight:bolder'>发动机型号</td>";
            var rowTransmission = "<tr><td style='font-weight:bolder'>变速箱型号</td>";
            var paiLiang = "<tr><td style='font-weight:bolder'>排量</td>";
            var startProductYear = "<tr><td style='font-weight:bolder'>开始生产年份</td>";
            var endProductYear = "<tr><td style='font-weight:bolder'>停产年份</td>";
            var nian = "<tr><td style='font-weight:bolder'>年款</td>";
            var salesName = "<tr><td style='font-weight:bolder'>版型</td>";
            var cylinderNumber = "<tr><td style='font-weight:bolder'>气缸数</td>";
            var fuelType = "<tr><td style='font-weight:bolder'>燃油类型</td>";
            var guidePrice = "<tr><td style='font-weight:bolder'>指导价(万元)</td>";
            var carBody = "<tr><td style='font-weight:bolder'>车身结构</td>";
            var chassisNo = "<tr><td style='font-weight:bolder'>底盘编号</td>";
            var transmissionTypeE = "<tr><td style='font-weight:bolder'>变速箱类型(英文)</td>";
            var transmissionTypeC = "<tr><td style='font-weight:bolder'>变速箱类型(中文)</td>";
            var powerKw = "<tr><td style='font-weight:bolder'>功率(KW)</td>";
            var valveNumPerCylinder = "<tr><td style='font-weight:bolder'>每缸气门数</td>";
            var airIntakeMode = "<tr><td style='font-weight:bolder'>进气形式</td>";
            var driveType = "<tr><td style='font-weight:bolder'>驱动方式</td>";
            var steeringType = "<tr><td style='font-weight:bolder'>助力转向类型</td>";
            var frontBrakeType = "<tr><td style='font-weight:bolder'>前制动器类型</td>";
            var backBrakeType = "<tr><td style='font-weight:bolder'>后制动器类型</td>";
            var highBeamType = "<tr><td style='font-weight:bolder'>远光灯类型</td>";
            var dippedHeadlightType = "<tr><td style='font-weight:bolder'>近光灯类型</td>";
            var fuelFilterType = "<tr><td style='font-weight:bolder'>燃油滤类型</td>";
            var frontTireSize = "<tr><td style='font-weight:bolder'>前胎规格</td>";
            var rearTireSize = "<tr><td style='font-weight:bolder'>后胎规格</td>";
            var oilSupplyMode = "<tr><td style='font-weight:bolder'>供油方式</td>";
            var alloyWheel = "<tr><td style='font-weight:bolder'>铝合金轮毂</td>";
            var tireMonitorSystem = "<tr><td style='font-weight:bolder'>胎压监测装置</td>";
            var autoStartStopSys = "<tr><td style='font-weight:bolder'>发动机启停技术</td>";
            var origialTire = "<tr><td style='font-weight:bolder'>原配轮胎产品(PID)</td>";
            var origialTireRemark = "<tr><td style='font-weight:bolder'>原配轮胎产品(备注)</td>";
            var frontSuspensionType = "<tr><td style='font-weight:bolder'>前悬架类型</td>";
            var rearSuspensionType = "<tr><td style='font-weight:bolder'>后悬架类型</td>";
            var column = 1;
            for(var i=0;i<data.length;i++){
                var item = data[i];
                var pids = "";
                var pidRemark = "";
                if(item.pid != null && item.pid.length > 0 ){
                    pids = item.pid.join(';');
                }
                if(item.noProductName != null && item.noProductName.length > 0 ){
                    pidRemark = item.noProductName.join(';');
                }
                var vehilceName = item.brand + " | " + item.vehicleSeries + " | " + item.paiLiang + " | " + item.listedYear + "-" + item.stopProductionYear + "年产 | " + item.nian + "款" + item.salesName + " | " + item.transmissionTypeE;
                rowTidId += '<th><span>Tid: '+item.tid+'</span>' + editStyle + operationTidDelete  + '</ul></th>';
                rowVehicleID += "<td><span>" + item.vehicleId + "</span></td>";
                rowStatus += "<td>" + timingVehicleAccessoryUpdate + "<span><a>" + this.convertStatusToChs(item.status) + "</a><a style=\"display:none\">Status</a></span>" + editStyle + statusDropDownListEdit + "</ul><a style=\"display:none\">" + column + "</a></td>";
                brandFromVehicleType += "<td><a>" + item.brandFromVehicleType + "</a>" + editStyle + operationEditVehicle + "</ul><a style=\"display:none\">" + column + "</a></td>";
                vehicleFromVehicleType += "<td><a>" + item.vehicleFromVehicleType + "</a>" + editStyle + operationEditVehicle + "</ul><a style=\"display:none\">" + column + "</a></td>";
                vehicleType += "<td><span>" + item.vehicleSeries + "</span></td>";
                rowVehicleName += "<td>" + vehilceName + "</td>";
                rowEngineNo += "<td>" + vehiclePartupdate + "<span><a>" + item.engineNo + "</a><a style=\"display:none\">EngineNo</a></span>" + editStyle + operationEdit + "</ul><a style=\"display:none\">" + column + "</a></td>";
                rowTransmission += "<td><span>" + item.transmission + "</span></td>";
                paiLiang += "<td>" + vehicleInfoInput + "<span><a>" + item.paiLiang + "</a><a style=\"display:none\">PaiLiang</a></span>" + editStyle + operationEdit + "</ul><a style=\"display:none\">" + column + "</a></td>";
                startProductYear += "<td>" + vehicleInfoInput + "<span><a>" + item.listedYear + "</a><a style=\"display:none\">ListedYear</a></span>" + editStyle + operationEdit + "</ul><a style=\"display:none\">" + column + "</a></td>";
                endProductYear += "<td>" + vehicleInfoInput + "<span><a>" + item.stopProductionYear + "</a><a style=\"display:none\">StopProductionYear</a></span>" + editStyle + operationEdit + "</ul><a style=\"display:none\">" + column + "</a></td>";
                nian += "<td>" + vehicleInfoInput + "<span><a>" + item.nian + "</a><a style=\"display:none\">Nian</a></span>" + editStyle + operationEdit + "</ul><a style=\"display:none\">" + column + "</a></td>";
                salesName += "<td>" + vehicleInfoInput + "<span><a>" + item.salesName + "</a><a style=\"display:none\">SalesName</a></span>" + editStyle + operationEdit + "</ul><a style=\"display:none\">" + column + "</a></td>";
                cylinderNumber += "<td>" + vehicleInfoInput + "<span><a>" + item.cylinderNumber + "</a><a style=\"display:none\">CylinderNumber</a></span>" + editStyle + operationEdit + "</ul><a style=\"display:none\">" + column + "</a></td>";
                fuelType += "<td>" + vehicleInfoInput + "<span><a>" + item.fuelType + "</a><a style=\"display:none\">FuelType</a></span>" + editStyle + operationEdit + "</ul><a style=\"display:none\">" + column + "</a></td>";
                guidePrice += "<td>" + vehicleInfoInput + "<span><a>" + item.guidePrice + "</a><a style=\"display:none\">GuidePrice</a></span>" + editStyle + operationEdit + "</ul><a style=\"display:none\">" + column + "</a></td>";
                carBody += "<td>" + vehiclePartupdate + "<span><a>" + item.carBody + "</a><a style=\"display:none\">CarBody</a></span>" + editStyle + operationEdit + "</ul><a style=\"display:none\">" + column + "</a></td>";
                chassisNo += "<td>" + vehiclePartupdate + "<span><a>" + item.chassisNo + "</a><a style=\"display:none\">ChassisNo</a></span>" + editStyle + operationEdit + "</ul><a style=\"display:none\">" + column + "</a></td>";
                transmissionTypeE += "<td>" + vehicleAccessoryUpdate + "<span><a>" + item.transmissionTypeE + "</a><a style=\"display:none\">TransmissionTypeE</a></span>" + editStyle + dropDownListEdit + "</ul><a style=\"display:none\">" + column + "</a></td>";
                transmissionTypeC += "<td>" + vehicleAccessoryUpdate + "<span><a>" + item.transmissionTypeC + "</a><a style=\"display:none\">TransmissionTypeC</a></span>" + editStyle + dropDownListEdit + "</ul><a style=\"display:none\">" + column + "</a></td>";
                powerKw += "<td>" + vehiclePartupdate + "<span><a>" + item.powerKw + "</a><a style=\"display:none\">PowerKw</a></span>" + editStyle + operationEdit + "</ul><a style=\"display:none\">" + column + "</a></td>";
                valveNumPerCylinder += "<td>" + vehiclePartupdate + "<span><a>" + item.valveNumPerCylinder + "</a><a style=\"display:none\">ValveNumPerCylinder</a></span>" + editStyle + operationEdit + "</ul><a style=\"display:none\">" + column + "</a></td>";
                airIntakeMode += "<td>" + vehicleAccessoryUpdate + "<span><a>" + item.airIntakeMode + "</a><a style=\"display:none\">AirIntakeMode</a></span>" + editStyle + dropDownListEdit + "</ul><a style=\"display:none\">" + column + "</a></td>";
                driveType += "<td>" + vehicleAccessoryUpdate + "<span><a>" + item.driveType + "</a><a style=\"display:none\">DriveType</a></span>" + editStyle + dropDownListEdit + "</ul><a style=\"display:none\">" + column + "</a></td>";
                steeringType += "<td>" + vehicleAccessoryUpdate + "<span><a>" + item.steeringType + "</a><a style=\"display:none\">SteeringType</a></span>" + editStyle + dropDownListEdit + "</ul><a style=\"display:none\">" + column + "</a></td>";
                frontBrakeType += "<td>" + vehicleAccessoryUpdate + "<span><a>" + item.frontBrakeType + "</a><a style=\"display:none\">FrontBrakeType</a></span>" + editStyle + dropDownListEdit + "</ul><a style=\"display:none\">" + column + "</a></td>";
                backBrakeType += "<td>" + vehicleAccessoryUpdate + "<span><a>" + item.backBrakeType + "</a><a style=\"display:none\">BackBrakeType</a></span>" + editStyle + dropDownListEdit + "</ul><a style=\"display:none\">" + column + "</a></td>";
                highBeamType += "<td>" + vehicleAccessoryUpdate + "<span><a>" + item.highBeamType + "</a><a style=\"display:none\">HighBeamType</a></span>" + editStyle + dropDownListEdit + "</ul><a style=\"display:none\">" + column + "</a></td>";
                dippedHeadlightType += "<td>" + vehicleAccessoryUpdate + "<span><a>" + item.dippedHeadlightType + "</a><a style=\"display:none\">DippedHeadlightType</a></span>" + editStyle + dropDownListEdit + "</ul><a style=\"display:none\">" + column + "</a></td>";
                fuelFilterType += "<td>" + vehicleAccessoryUpdate + "<span style='display:inline;'><a>" + item.fuelFilterType + "</a><a style=\"display:none\">FuelFilterType</a></span>" + editStyle + dropDownListEdit + "</ul><a style=\"display:none\">" + column + "</a></td>";
                frontTireSize += "<td>" + vehiclePartupdate + "<span><a>" + item.frontTireSize + "</a><a style=\"display:none\">FrontTireSize</a></span>" + editStyle + editVehicleTires + "</ul><a style=\"display:none\">" + column + "</a></td>";
                rearTireSize += "<td>" + vehiclePartupdate + "<span><a>" + item.rearTireSize + "</a><a style=\"display:none\">RearTireSize</a></span>" + editStyle + editVehicleTires + "</ul><a style=\"display:none\">" + column + "</a></td>";
                oilSupplyMode += "<td>" + vehicleAccessoryUpdate + "<span style='display:inline;'><a>" + item.oilSupplyMode + "</a><a style=\"display:none\">OilSupplyMode</a></span>" + editStyle + dropDownListEdit + "</ul><a style=\"display:none\">" + column + "</a></td>";
                alloyWheel += "<td>" + vehicleAccessoryUpdate + "<span style='display:inline;'><a>" + item.alloyWheel + "</a><a style=\"display:none\">AlloyWheel</a></span>" + editStyle + dropDownListEdit + "</ul><a style=\"display:none\">" + column + "</a></td>";
                tireMonitorSystem += "<td>" + vehicleAccessoryUpdate + "<span style='display:inline;'><a>" + item.tireMonitorSystem + "</a><a style=\"display:none\">TireMonitorSystem</a></span>" + editStyle + dropDownListEdit + "</ul><a style=\"display:none\">" + column + "</a></td>";
                autoStartStopSys += "<td>" + vehicleAccessoryUpdate + "<span style='display:inline;'><a>" + item.autoStartStopSys + "</a><a style=\"display:none\">AutoStartStopSys</a></span>" + editStyle + dropDownListEdit + "</ul><a style=\"display:none\">" + column + "</a></td>";
                origialTire += "<td><span><a>" + pids + "</a><a style=\"display:none\">Pid</a></span>" + editStyle + editTiresMatch + "</ul><a style=\"display:none\">" + column + "</a></td>";
                origialTireRemark += "<td><span><a>" + pidRemark + "</a><a style=\"display:none\">NoProductName</a></span>" + editStyle + editTiresMatch + "</ul><a style=\"display:none\">" + column + "</a></td>";
                frontSuspensionType += "<td><span style='display:inline;'><a>" + item.frontSuspensionType + "</a><a style=\"display:none\">FrontSuspensionType</a></span>" + editStyle + dropDownListEdit + "</ul><a style=\"display:none\">" + column + "</a></td>";
                rearSuspensionType += "<td><span style='display:inline;'><a>" + item.rearSuspensionType + "</a><a style=\"display:none\">RearSuspensionType</a></span>" + editStyle + dropDownListEdit + "</ul><a style=\"display:none\">" + column + "</a></td>";
                column ++;
            }
            rowTidId +="</tr>";
            rowVehicleID +="</tr>";
            rowStatus +="</tr>";
            brandFromVehicleType +="</tr>";
            vehicleFromVehicleType +="</tr>";
            vehicleType +="</tr>";
            rowVehicleName +="</tr>";
            rowEngineNo +="</tr>";
            rowTransmission +="</tr>";
            paiLiang +="</tr>";
            startProductYear +="</tr>";
            endProductYear +="</tr>";
            nian +="</tr>";
            salesName +="</tr>";
            cylinderNumber +="</tr>";
            fuelType +="</tr>";
            guidePrice +="</tr>";
            carBody +="</tr>";
            chassisNo +="</tr>";
            transmissionTypeE +="</tr>";
            transmissionTypeC +="</tr>";
            powerKw +="</tr>";
            valveNumPerCylinder +="</tr>";
            airIntakeMode +="</tr>";
            driveType +="</tr>";
            steeringType +="</tr>";
            frontBrakeType +="</tr>";
            backBrakeType +="</tr>";
            highBeamType +="</tr>";
            dippedHeadlightType +="</tr>";
            fuelFilterType +="</tr>";
            frontTireSize +="</tr>";
            rearTireSize +="</tr>";
            oilSupplyMode +="</tr>";
            alloyWheel +="</tr>";
            tireMonitorSystem +="</tr>";
            autoStartStopSys +="</tr>";
            origialTire +="</tr>";
            origialTireRemark +="</tr>";
            frontSuspensionType +="</tr>";
            rearSuspensionType +="</tr>";
            html =rowTidId +rowVehicleID +rowStatus +brandFromVehicleType +vehicleFromVehicleType +vehicleType +rowVehicleName +rowEngineNo +rowTransmission +paiLiang +startProductYear +endProductYear +nian +salesName +cylinderNumber +fuelType +guidePrice +carBody +chassisNo +transmissionTypeE +transmissionTypeC +powerKw +valveNumPerCylinder +airIntakeMode +driveType +steeringType +frontBrakeType +backBrakeType +highBeamType +dippedHeadlightType +fuelFilterType +frontTireSize +rearTireSize +oilSupplyMode +alloyWheel +tireMonitorSystem +autoStartStopSys +origialTire +origialTireRemark +frontSuspensionType +rearSuspensionType;
            this.innerHtmlData = html;
        },
        showLoading(){
            document.getElementById("loadingData").style.display = "";
            document.getElementById("vehicleData").style.display = "none";
            document.getElementById("noData").style.display = "none";
        },
        hideLoading(){
            document.getElementById("loadingData").style.display = "none";
        },
        convertStatusToChs(engStr){
            var chsStr = "";
            switch(engStr){
                 case "New":
                chsStr = "已创建";
                break;
            case "Verified":
                chsStr = "已审核";
                break;
            case "Active":
                chsStr = "已上架";
                break;
            case "Deleted":
                chsStr = "已下架";
                break;
            }
            return chsStr;
        },
        showEdit(){
            var elements = document.getElementsByClassName("operation-group");
            if(this.checked){
                for(var i=0;i<elements.length;i++){
                    elements[i].style.display = "";
                }
            }
            else{
                for(var i=0;i<elements.length;i++){
                    elements[i].style.display = "none";
                }
            }
        },
        showOperation(){
          var display="display:none";
          if(this.checked){
            display=""
          }
          return display;
        },
        resetData(){
            this.vehicleIdAdd = '';
            this.transmissionType='暂无';
            this.addVehicleRequest={
                brand:'',
                vehicle:'',
                vehicleId:'',
                vehicleSeries:'',
                engineNo:'',
                paiLiang:'',
                nian:'',
                listedYear:'',
                stopProductionYear:'',
                salesName:'',
                cylinderNumber:'',
                valveNumPerCylinder:'',
                fuelType:'',
                guidePrice:'',
                powerKw:'',
                status:'New',
                transmissionTypeE:'',
                transmissionTypeC:'',
                transmission:'',
                carBody:'',
                airIntakeMode:'暂无',
                driveType:'暂无',
                chassisNo:'',
                steeringType:'暂无',
                frontBrakeType:'暂无',
                backBrakeType:'暂无',
                dippedHeadlightType:'',
                highBeamType:'',
                fuelFilterType:'暂无',
                frontTireSize:'',
                rearTireSize:'',
                oilSupplyMode:'暂无',
                alloyWheel:'无',
                tireMonitorSystem:'无',
                autoStartStopSys:'无',
                frontSuspensionType:'',
                rearSuspensionType:'',
                originalPid:'',
                originalName:''
            };
        }
    }
}
</script>

<style lang="scss" scoped>
.input{
    width: 200px;
}
/deep/ .el-dialog{
  display: flex;
  flex-direction: column;
  margin:0 !important;
  position:absolute;
  top:50%;
  left:50%;
  transform:translate(-50%,-50%);
  /*height:600px;*/
  max-height:calc(100% - 30px);
  max-width:calc(100% - 30px);
}
/deep/ .el-dialog .el-dialog__body{
  flex:1;
  overflow: auto;
}
/deep/ .el-dialog__footer{
    padding:10px 20px 10px;
}
/deep/ .el-dialog__body{
    padding:20px 20px;
}

.container {
    margin-left: 30px;
    .input-label{
    margin-left:4px;
    font-size:14px;
    }

    .titleSpan{
        width: 143px;
        // text-align: right;
        font-weight: normal;
        margin-top: 10px;
    };
    .contentSpan{
        width: 220px;
        font-weight: normal;
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