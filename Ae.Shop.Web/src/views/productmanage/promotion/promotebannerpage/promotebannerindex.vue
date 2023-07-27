<template>
  <main class="container">
    <!-- 首页 -->
    <section>
      <rg-page
        background
        id="indexPage"
        :pageIndex="condition.pageIndex"
        :pageSize="condition.pageSize"
        :dataCount="totalList"
        :tableLoading="tableLoading"
        :tableData="tableData"
        :pageChange="pageChange"
        :headerBtnWidth="180"
        :searching="search"
      >
        <template v-slot:condition>
          <el-form-item>
            <el-input
              class="input"
              clearable
              placeholder="请输入标题"
              prefix-icon="el-icon-search"
              v-model="condition.title"
            ></el-input>
          </el-form-item>
          <el-form-item label="活动时间">
            <el-date-picker
              style="width: 160px;"
              v-model="condition.startTime"
              type="date"
              placeholder="开始日期"
              size="mini"
            ></el-date-picker>
          </el-form-item>
          <el-form-item>
            <el-date-picker
              style="width: 160px;"
              v-model="condition.endTime"
              type="date"
              placeholder="终止日期"
              size="mini"
            ></el-date-picker>
          </el-form-item>
          <el-form-item prop="status" label="终端类型">
            <el-select
              v-model="condition.terminalType"
              size="mini"
              style="width:100px;"
            >
              <el-option
                v-for="item in terminalTypeSel"
                :key="item.value"
                :label="item.label"
                :value="item.value"
              ></el-option>
            </el-select>
          </el-form-item>
        </template>
        <template v-slot:header_btn>
          <el-button
            type="primary"
            size="mini"
            icon="el-icon-plus"
            @click="showCreate()"
            >新增</el-button
          >
        </template>

        <template v-slot:tb_cols>
          <!-- <el-table-column
            label="编号"
            prop="id"
            align="center"
          ></el-table-column> -->
          <el-table-column
            label="活动编码"
            prop="code"
            align="center"
          ></el-table-column>
          <el-table-column
            label="标题"
            prop="title"
            align="center"
          ></el-table-column>

          <el-table-column label="类型" align="center">
            <template slot-scope="scope">{{
              displayTypeList[scope.row.type]
            }}</template>
          </el-table-column>
          <el-table-column
            label="开始时间"
            prop="startTime"
            align="center"
          ></el-table-column>
          <el-table-column
            label="结束时间"
            prop="endTime"
            align="center"
          ></el-table-column>
          <el-table-column label="终端类型" align="center">
            <template slot-scope="scope">{{
              terminalTypeList[scope.row.terminalType]
            }}</template>
          </el-table-column>
          <el-table-column label="状态" align="center">
            <template slot-scope="scope">
              <label v-if="scope.row.isOnline == 1">上架</label>
              <label style="color:red" v-else>下架</label>
            </template>
          </el-table-column>
          <el-table-column
            label="创建人"
            prop="createBy"
            align="center"
          ></el-table-column>
          <el-table-column
            label="创建时间"
            prop="createTime"
            align="center"
          ></el-table-column>

          <el-table-column
            label="操作"
            width="150px"
            align="center"
            fixed="right"
          >
            <template slot-scope="scope">
              <!-- <el-button
                size="mini"
                type="primary"
                style="padding:10px 7px;"
                @click="viewDetail(scope.row)"
                >详情</el-button
              > -->
              <el-button
                size="mini"
                type="primary"
                style="padding:10px 7px;"
                @click="viewDetail(scope.row)"
                >编辑</el-button
              >
              <el-button
                size="mini"
                type="danger"
                style="padding:10px 7px;"
                v-if="scope.row.isOnline == 1"
                @click="deleteConfigAdvertisement(scope.row, 0)"
                >下架</el-button
              >

              <el-button
                size="mini"
                type="warning"
                style="padding:10px 7px;"
                v-if="scope.row.isOnline == 0"
                @click="deleteConfigAdvertisement(scope.row, 1)"
                >上架</el-button
              >
            </template>
          </el-table-column>
        </template>
      </rg-page>
    </section>
    <!-- 首页 -->

    <section id="create">
      <rg-dialog
        :title="formTitle"
        :visible.sync="formVisible"
        v-if="formVisible"
        :beforeClose="cancel"
        :btnCancel="{
          label: '关闭',
          click: done => {
            cancel('formModel');
          }
        }"
        :btnRemove="false"
        :footbar="true"
        width="78%"
        maxWidth="800px"
        minWidth="600px"
      >
        <template v-slot:content>
          <el-form
            :model="formModel"
            :rules="rules"
            ref="formModel"
            size="small"
          >
            <el-row>
              <el-col :span="24">
                <el-form-item
                  prop="code"
                  :label-width="formLabelWidth"
                  label="活动编码"
                >
                  <el-input
                    v-model="formModel.code"
                    placeholder="请输入活动编码"
                  />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="24">
                <el-form-item
                  prop="title"
                  :label-width="formLabelWidth"
                  label="标题"
                >
                  <el-input
                    v-model="formModel.title"
                    placeholder="请输入标题"
                  />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="12">
                <el-form-item
                  prop="imageUrl"
                  :label-width="formLabelWidth"
                  label="缩图地址"
                >
                  <el-upload
                    v-if="!formModel.imageUrl"
                    style="float: left"
                    action="http://upload.qiniup.com/"
                    :before-upload="beforeAvatarUpload"
                    :show-file-list="false"
                    accept="image/jpeg, image/png, image/jpg"
                    :http-request="uploadThumbnail"
                  >
                    <el-button size="mini" type="primary">点击上传</el-button>
                  </el-upload>
                  <div class="img-info-area">
                    <div class="pre-image-area"  v-if="formModel.imageUrl">
                      <el-image
                        class="img"
                        :src="imgHost+formModel.imageUrl" 
                        :preview-src-list="[imgHost+formModel.imageUrl]">
                      </el-image>
                      <i class="el-icon-error del" @click="delImageValue('imageUrl')"></i>
                    </div>
                    <div v-if="formModel.imageUrl" class="single-line-text" :title="formModel.imageUrl">{{formModel.imageUrl}}</div>
                  </div>
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item
                  prop="routeUrl"
                  :label-width="formLabelWidth"
                  label="路由地址"
                >
                  <el-input
                    v-model="formModel.routeUrl"
                    placeholder="请输入路由地址"
                  />
                </el-form-item>
              </el-col>
            </el-row>

            <el-row>
              <el-col :span="12">
                <el-form-item
                  prop="type"
                  :label-width="formLabelWidth"
                  label="展示方式"
                >
                  <el-select
                    v-model="formModel.type"
                    clearable
                    filterable
                    placeholder="请选择展示方式"
                  >
                    <el-option
                      v-for="item in displayTypeSel"
                      :key="item.value"
                      :label="item.label"
                      :value="item.value"
                    ></el-option>
                  </el-select>
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item
                  prop="terminalType"
                  :label-width="formLabelWidth"
                  label="终端类型"
                >
                  <el-select
                    v-model="formModel.terminalType"
                    size="mini"
                    placeholder="请选择终端类型"
                  >
                    <el-option
                      v-for="item in terminalTypeSel"
                      :key="item.value"
                      :label="item.label"
                      :value="item.value"
                    ></el-option>
                  </el-select>
                </el-form-item>
              </el-col>
            </el-row>

            <el-row>
              <el-col :span="12">
                <el-form-item
                  prop="rank"
                  :label-width="formLabelWidth"
                  label="排序"
                >
                  <el-input v-model="formModel.rank" placeholder="请输入排序" />
                </el-form-item>
              </el-col>

              <el-col :span="12">
                <el-form-item :label-width="formLabelWidth" label="扩展参数">
                  <el-input
                    v-model="formModel.extendId"
                    placeholder="请扩展参数"
                  />
                </el-form-item>
              </el-col>
            </el-row>

            <el-row>
              <el-col :span="24">
                <el-form-item
                  prop="times"
                  :label-width="formLabelWidth"
                  label="活动时间"
                >
                  <el-date-picker
                    v-model="formModel.times"
                    type="datetimerange"
                    :picker-options="pickerOptions"
                    range-separator="至"
                    start-placeholder="开始日期"
                    end-placeholder="结束日期"
                    align="right"
                  >
                  </el-date-picker>
                </el-form-item>
              </el-col>
            </el-row>

             <el-row>
              <el-col :span="12">
                <el-form-item
                  prop="beginImageUrl"
                  :label-width="formLabelWidth"
                  label="详图地址"
                >
                  <el-upload
                    v-if="!formModel.beginImageUrl"
                    style="float: left"
                    action="http://upload.qiniup.com/"
                    :before-upload="beforeAvatarUpload"
                    :show-file-list="false"
                    accept="image/jpeg, image/png, image/jpg"
                    :http-request="uploadDetail"
                  >
                    <el-button size="mini" type="primary">点击上传</el-button>
                  </el-upload>
                  <div class="img-info-area">
                    <div class="pre-image-area"  v-if="formModel.beginImageUrl">
                      <el-image
                        class="img"
                        :src="imgHost+formModel.beginImageUrl" 
                        :preview-src-list="[imgHost+formModel.beginImageUrl]">
                      </el-image>
                      <i class="el-icon-error del" @click="delImageValue('beginImageUrl')"></i>
                    </div>
                    <div v-if="formModel.beginImageUrl" class="single-line-text" :title="formModel.beginImageUrl">{{formModel.beginImageUrl}}</div>
                  </div>
                </el-form-item>
              </el-col>
              <el-col :span="12">
                 <el-form-item
                  prop="endImageUrl"
                  :label-width="formLabelWidth"
                  label="尾图地址"
                >
                  <el-upload
                    v-if="!formModel.endImageUrl"
                    style="float: left"
                    action="http://upload.qiniup.com/"
                    :before-upload="beforeAvatarUpload"
                    :show-file-list="false"
                    accept="image/jpeg, image/png, image/jpg"
                    :http-request="uploadTail"
                  >
                    <el-button size="mini" type="primary">点击上传</el-button>
                  </el-upload>
                  <div class="img-info-area">
                    <div class="pre-image-area"  v-if="formModel.endImageUrl">
                      <el-image
                        class="img"
                        :src="imgHost+formModel.endImageUrl" 
                        :preview-src-list="[imgHost+formModel.endImageUrl]">
                      </el-image>
                      <i class="el-icon-error del" @click="delImageValue('endImageUrl')"></i>
                    </div>
                    <div v-if="formModel.endImageUrl" class="single-line-text" :title="formModel.endImageUrl">{{formModel.endImageUrl}}</div>
                  </div>
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="12">
                <el-form-item
                  prop="btnImageUrl"
                  :label-width="formLabelWidth"
                  label="按钮图片1"
                >
                  <el-upload
                    v-if="!formModel.btnImageUrl"
                    style="float: left"
                    action="http://upload.qiniup.com/"
                    :before-upload="beforeAvatarUpload"
                    :show-file-list="false"
                    accept="image/jpeg, image/png, image/jpg"
                    :http-request="uploadBtn1"
                  >
                    <el-button size="mini" type="primary">点击上传</el-button>
                  </el-upload>
                  <div class="img-info-area">
                    <div class="pre-image-area"  v-if="formModel.btnImageUrl">
                      <el-image
                        class="img"
                        :src="imgHost+formModel.btnImageUrl" 
                        :preview-src-list="[imgHost+formModel.btnImageUrl]">
                      </el-image>
                      <i class="el-icon-error del" @click="delImageValue('btnImageUrl')"></i>
                    </div>
                    <div v-if="formModel.btnImageUrl" class="single-line-text" :title="formModel.btnImageUrl">{{formModel.btnImageUrl}}</div>
                  </div>
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item
                  prop="gotoUrl"
                  :label-width="formLabelWidth"
                  label="按钮跳转路由1"
                >
                  <el-input
                    v-model="formModel.gotoUrl"
                    placeholder="请输入路由地址"
                  />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="12">
                <el-form-item
                  prop="btn2ImageUrl"
                  :label-width="formLabelWidth"
                  label="按钮图片2"
                >
                  <el-upload
                    v-if="!formModel.btn2ImageUrl"
                    style="float: left"
                    action="http://upload.qiniup.com/"
                    :before-upload="beforeAvatarUpload"
                    :show-file-list="false"
                    accept="image/jpeg, image/png, image/jpg"
                    :http-request="uploadBtn2"
                  >
                    <el-button size="mini" type="primary">点击上传</el-button>
                  </el-upload>
                  <div class="img-info-area">
                    <div class="pre-image-area"  v-if="formModel.btn2ImageUrl">
                      <el-image
                        class="img"
                        :src="imgHost+formModel.btn2ImageUrl" 
                        :preview-src-list="[imgHost+formModel.btn2ImageUrl]">
                      </el-image>
                      <i class="el-icon-error del" @click="delImageValue('btn2ImageUrl')"></i>
                    </div>
                    <div v-if="formModel.btn2ImageUrl" class="single-line-text" :title="formModel.btn2ImageUrl">{{formModel.btn2ImageUrl}}</div>
                  </div>
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item
                  prop="goto2Url"
                  :label-width="formLabelWidth"
                  label="按钮跳转路由2"
                >
                  <el-input
                    v-model="formModel.goto2Url"
                    placeholder="请输入路由地址"
                  />
                </el-form-item>
              </el-col>
            </el-row>
            <el-row>
              <el-col :span="12">
                <el-form-item
                  prop="btn3ImageUrl"
                  :label-width="formLabelWidth"
                  label="按钮图片3"
                >
                  <el-upload
                    v-if="!formModel.btn3ImageUrl"
                    style="float: left"
                    action="http://upload.qiniup.com/"
                    :before-upload="beforeAvatarUpload"
                    :show-file-list="false"
                    accept="image/jpeg, image/png, image/jpg"
                    :http-request="uploadBtn3"
                  >
                    <el-button size="mini" type="primary">点击上传</el-button>
                  </el-upload>
                  <div class="img-info-area">
                    <div class="pre-image-area"  v-if="formModel.btn3ImageUrl">
                      <el-image
                        class="img"
                        :src="imgHost+formModel.btn3ImageUrl" 
                        :preview-src-list="[imgHost+formModel.btn3ImageUrl]">
                      </el-image>
                      <i class="el-icon-error del" @click="delImageValue('btn3ImageUrl')"></i>
                    </div>
                    <div v-if="formModel.btn3ImageUrl" class="single-line-text" :title="formModel.btn3ImageUrl">{{formModel.btn3ImageUrl}}</div>
                  </div>
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item
                  prop="goto3Url"
                  :label-width="formLabelWidth"
                  label="按钮跳转路由3"
                >
                  <el-input
                    v-model="formModel.goto3Url"
                    placeholder="请输入路由地址"
                  />
                </el-form-item>
              </el-col>
            </el-row>
          </el-form>
        </template>
        <template v-slot:footer>
          <el-button
            icon="el-icon-check"
            size="mini"
            type="primary"
            @click="save('formModel')"
            >保存</el-button
          >
        </template>
      </rg-dialog>
    </section>
  </main>
</template>

<script>
import { Message, MessageBox } from "element-ui";
import { validateURL } from "@/utils/validate";
import { upload } from "@/utils/uploadfile";
import { appSvc } from "@/api/product/promotebanner";
import { isNumber } from "util";
export default {
  data() {
    return {
      pickerOptionsStart: {},
      pickerOptions: {
        disabledDate(time) {
          var curDate = new Date();
          var preDate = new Date(curDate.getTime() - 24 * 60 * 60 * 1000);
          return time.getTime() <= preDate.getTime();
        },
        shortcuts: [
          {
            text: "最近一周",
            onClick(picker) {
              const end = new Date();
              const start = new Date();
              start.setTime(start.getTime() - 3600 * 1000 * 24 * 7);
              picker.$emit("pick", [start, end]);
            }
          },
          {
            text: "最近一个月",
            onClick(picker) {
              const end = new Date();
              const start = new Date();
              start.setTime(start.getTime() - 3600 * 1000 * 24 * 30);
              picker.$emit("pick", [start, end]);
            }
          },
          {
            text: "最近三个月",
            onClick(picker) {
              const end = new Date();
              const start = new Date();
              start.setTime(start.getTime() - 3600 * 1000 * 24 * 90);
              picker.$emit("pick", [start, end]);
            }
          }
        ]
      },
      input: undefined,
      deleteFormModel: {
        id: undefined,
        isOnline: undefined
      },
      readonly: true,
      tableLoading: false,
      currentPage: 1,

      //table页面的条件
      condition: {
        pageIndex: 1,
        pageSize: 10,
        title: undefined,
        startTime: undefined,
        endTime: undefined,
        terminalType: undefined
      },
      terminalTypeSel: [
        {
          value: "ShopApp",
          label: "管家"
        },
        {
          value: "CbJApplet",
          label: "（B端）"
        },
        {
          value: "YcJApplet",
          label: "（C端）"
        },
        {
          value: "QpJApplet",
          label: "汽配"
        }
      ],
      displayTypeList: {
        Top: "顶部",
        Bottom: "底部"
      },
      terminalTypeList: {
        ShopApp: "管家",
        CbJApplet: "B端",
        YcJApplet: "C端",
        QpJApplet: "汽配"
      },
      displayTypeSel: [
        {
          value: "Top",
          label: "顶部"
        },
        {
          value: "Bottom",
          label: "底部"
        }
      ],
      detailCondition: { id: undefined },

      imgHost:'https://m.aerp.com.cn/',
      formModel: {
        id: undefined,
        code: undefined,
        title: undefined,
        imageUrl: undefined,
        routeUrl: undefined,
        type: undefined,
        terminalType: undefined,
        times: [],
        rank: 0,
        extendId: "",

        beginImageUrl:undefined,
        endImageUrl:undefined,

        btnImageUrl:undefined,
        gotoUrl: undefined,
        btn2ImageUrl:undefined,
        goto2Url: undefined,
        btn3ImageUrl:undefined,
        goto3Url: undefined,
      },

      formModelInit: {
        id: undefined,
        code: undefined,
        title: undefined,
        imageUrl: undefined,
        routeUrl: undefined,
        type: undefined,
        terminalType: undefined,
        times: [],
        rank: 0,
        extendId: "",

        beginImageUrl:undefined,
        endImageUrl:undefined,
        btnImageUrl:undefined,
        gotoUrl: undefined,
        btn2ImageUrl:undefined,
        goto2Url: undefined,
        btn3ImageUrl:undefined,
        goto3Url: undefined,
      },
      tableData: [],
      totalList: 0,
      formVisible: false,
      formIsCreated: true,
      formTitle: "新增",
      rules: {
        code: [{ required: true, message: "请输入活动编码", trigger: "blur" }],
        title: [{ required: true, message: "请输入标题", trigger: "blur" }],

        imageUrl: [
          { required: true, message: "请输入图片地址", trigger: "blur" }
        ],
        rank: [
          {
            required: true,
            trigger: "blur",
            validator: (r, v, callback) => {
              var reg = /^\d+$/;
              if (!reg.test(this.formModel.rank))
                callback(new Error("排序输入有误，大于等于0整数"));
              else callback();
            }
          }
        ],
        type: [{ required: true, message: "请选择展示类型", trigger: "blur" }],
        terminalType: [
          { required: true, message: "请选择终端类型", trigger: "blur" }
        ],
        times: [{ required: true, message: "请选择活动时间", trigger: "blur" }]
      },
      formLabelWidth: "120px"
    };
  },
  computed: {
    routeComputed: {
      get: function() {
        return this.formModel.route;
      },
      set: function(val) {
        this.formModel.route = val.toLowerCase();
      }
    }
  },
  created() {
    //页面初始化函数
    this.fetchData();
  },
  methods: {
    beforeAvatarUpload(file) {
      if(!this.formModel.code){
        this.$message.error("请先确定活动编码!");
        return false;
      }
      const isLt2M = file.size / 1024 / 1024 < 5;
      if (!isLt2M) {
        this.$message.error("上传图片大小不能超过 5MB!");
      }
      return isLt2M;
    },
    delImageValue(dataKey){//数据
      if(dataKey){
        this.$confirm("是否删除该图片?", {
          confirmButtonText: "确定",
          cancelButtonText: "取消",
          type: "info"
        }).then(() => {
          this.formModel[dataKey] = undefined
        }).catch(() => {});
      }
    },
    uploadThumbnail(request){//缩图
      this.uploadRequest(request,'imageUrl')
    },
    uploadDetail(request){//详图
      this.uploadRequest(request,'beginImageUrl')
    },
    uploadTail(request){//尾图
      this.uploadRequest(request,'endImageUrl')
    },
    uploadBtn1(request){//btn1
      this.uploadRequest(request,'btnImageUrl')
    },
    uploadBtn2(request){//btn2
      this.uploadRequest(request,'btn2ImageUrl')
    },
    uploadBtn3(request){//btn3
      this.uploadRequest(request,'btn3ImageUrl')
    },
    uploadRequest(request,dataKey) {
      const loading = this.$loading({
        lock: true,
        text: "拼命上传中......",
        spinner: "el-icon-loading",
        background: "rgba(0, 0, 0, 0.7)",
      });
      var fileName = request.file.name;
      var fileExtension = fileName.split(".").pop();
      const newFileName =
        appSvc.formatDate(new Date(), "yyyyMMddhhmmss") +
        appSvc.getRandomInt(1000, 9999) +
        "." +
        fileExtension;
      var directoryName = `miniApp/Banner/${this.formModel.code}`;
      var key = directoryName + "/" + newFileName;
      this.getToken(directoryName, fileName)
        .then((res) => {
          var result = res.data;
          const token = result.token;
          const host = result.host;
          upload(
            token,
            key,
            request,
            (next) => {
              // const total = next.total;
            },
            (error) => {
              // this.fileList = [];
            },
            (complete) => {
              const hash = complete.hash;
              const key = complete.key;
              var url = host + "/" + key;
              if(dataKey){
                this.formModel[dataKey] = key
              }
            }
          );
        })
        .catch(() => {})
        .finally(() => {
          loading.close();
        });
    },
     //获取token
    getToken(directoryName, fileName) {
      return appSvc.getQiNiuToken({
        directory: directoryName,
        fileName: fileName,
      });
    },
    viewDetail(row) {
      this.formVisible = true;
      this.formTitle = "修改Banner配置";
      this.detailCondition.id = row.id;

      appSvc
        .getConfigAdvertisement(this.detailCondition)
        .then(res => {
          this.formModel = res.data;
        })
        .catch(error => {
          console.log(error);
        });
      //updateConfigAdvertisement
    },
    showCreate() {
      this.formVisible = true;
      this.formTitle = "新增Banner配置";
      16;
    },
    save(formName) {
      var vm = this;

      this.$refs[formName].validate(valid => {
        if (valid) {
          if (this.formModel.id > 0) {
            debugger;
            appSvc
              .updateConfigAdvertisement(this.formModel)
              .then(res => {
                if (res.code == 10000) {
                  this.$message({
                    message: res.message,
                    type: "success"
                  });
                  vm.search();
                  this.formVisible = false;
                  vm.cancel();
                }
              })
              .catch(error => {
                console.log(error);
              });
          } else {
            appSvc
              .addConfigAdvertisement(this.formModel)
              .then(res => {
                if (res.code == 10000) {
                  this.$message({
                    message: res.message,
                    type: "success"
                  });
                  vm.search();
                  this.formVisible = false;
                  vm.cancel();
                }
              })
              .catch(error => {
                console.log(error);
              });
          }
        } else {
          return false;
        }
      });
    },
    cancel(formName) {
      this.formVisible = false;
      // this.resetSendForm();
      this.formModel = JSON.parse(JSON.stringify(this.formModelInit));
      var frmName =
        typeof formName === "string" && formName ? formName : "formModel";
      this.$refs[frmName].resetFields();
    },

    deleteConfigAdvertisement(row, status) {
      var msg = status == 1 ? "上架" : "下架";

      this.$confirm("确定" + msg + "吗?", "警告", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          var vm = this;
          this.deleteFormModel.id = row.id;
          this.deleteFormModel.isOnline = status;
          appSvc
            .deleteConfigAdvertisement(this.deleteFormModel)
            .then(
              res => {
                if (res.message != "") {
                  this.$message({
                    message: res.message,
                    type: "success"
                  });
                }

                vm.search();
                vm.cancel();
              },
              error => {
                console.log(error);
              }
            )
            .catch(() => {});
        })
        .catch(() => {});
    },

    fetchData() {
      appSvc
        .getConfigAdvertisements(this.condition)
        .then(
          res => {
            //  this.tableData = res.data;
            var data = res.data;

            this.tableData = data.items;
            this.totalList = data.totalItems;
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {});
    },

    pageChange(p) {
      this.currentPage = p.currentPage;
      this.condition.pageIndex = p.pageIndex;
      this.condition.pageSize = p.pageSize;
      this.getPaged();
    },
    pageTurning() {
      this.condition.pageIndex = this.currentPage;
      this.getPaged();
    },

    getPaged(flag) {
      this.tableLoading = true;

      console.log("condition: " + JSON.stringify(this.condition));
      appSvc
        .getConfigAdvertisements(this.condition)
        .then(
          res => {
            var data = res.data;
            this.tableData = data.items;
            this.totalList = data.totalItems;

            if (flag) {
              if (res.message != "") {
                this.$message({
                  message: res.message,
                  type: "success"
                });
              }
            }
          },
          error => {
            console.log(error);
          }
        )
        .catch(() => {})
        .finally(() => {
          console.log("tableLoading: false");
          this.tableLoading = false;
        });
    },
    search(flag) {
      this.condition.pageIndex = this.currentPage = 1;
      this.getPaged(flag);
    }
  }
};
</script>
<style lang="scss"></style>
<style lang="scss" scoped>
.container {
  margin-left: 30px;
  .bodyTop {
    height: 88px;
    display: flex;
    align-items: center;
  }
  .pagination {
    display: flex;
    align-items: center;
    justify-content: center;
    height: 100px;
  }
}
>>> .el-dialog__wrapper .rg-dialog .dialog_body {
  padding: 20px 40px;
  height: 500px;
  overflow: auto;
}
.el-range-editor--small.el-input__inner{
  width: 100%;
}
.el-select{
  width:100%
}
>>> .header-search,
.search-line {
  padding-bottom: 10px;
}
.el-row{
  margin-bottom: 4px;
}
.img-info-area{
  display:flex;
  height: 35px;
}
.pre-image-area{
  width: 35px;
  height: 35px;
  display: inline-block;
  position: relative;
  .img{
    width: 100%;
    height: 100%;
  }
  .del{
    position: absolute;
    top: 2px;
    right: 2px;
    color: #ff0000;
    cursor: pointer;
  }
}
.single-line-text{
  flex: 1;
  margin-bottom: -8px;
  align-self: end;
  overflow: hidden;
  text-overflow:ellipsis;
  white-space: nowrap;
}
</style>
