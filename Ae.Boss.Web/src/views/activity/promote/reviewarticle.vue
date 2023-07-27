<template>
  <main class="container">
    <div>
      <iframe
        :src="url"
        width="416"
        height="736"
        frameborder="0"
        scrolling="auto"
      />
      <div style="float:right;margin-top:10%;margin-right:50%">
        <el-tag type="success">可使用手机微信扫码查看预览</el-tag>
        <div id="qrCode" ref="qrCodeDiv" />
      </div>
    </div>
  </main>
</template>

<script>
import QRCode from 'qrcodejs2'
export default {
  data() {
    return {
      url: ''
    }
  },
  mounted: function() {
    this.$nextTick(function() {
      this.bindQRCode()
    })
  },
  created() {
    this.url = process.env.VUE_APP_BASE_SPREAD_DOMAIN + '/#/?id=' + this.$route.query.id + '&shopId=7&employeeId=46661238-70a0-482d-aa16-bbd710880257&isPreview=true'
  },
  methods: {
    bindQRCode: function() {
      new QRCode(this.$refs.qrCodeDiv, {
        text: this.url,
        width: 200,
        height: 200,
        colorDark: '#333333', // 二维码颜色
        colorLight: '#ffffff', // 二维码背景色
        correctLevel: QRCode.CorrectLevel.L// 容错率，L/M/H
      })
    }
  }
}
</script>
