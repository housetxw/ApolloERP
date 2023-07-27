let productUtil = {
  //copy 数据
  copyDeep:function(templateData) {
    // templateData 是要复制的数组或对象，这样的数组或者对象就是指向新的地址的
    return JSON.parse(JSON.stringify(templateData));
  },
  /**
     * newList 新数据
     * oldList 旧数据
     * compareArr array 需要检查的字段
     * primaryKey 主键，原数组与拼装数组的主键相同时才会产生
     * return 返回检查后的结果集  newList 和  oldList 结构必须一样
     */
  checkData: function (newList, oldList, compareArr, primaryKey) {
    if (!compareArr || !newList) {
      return [];
    }
    var fixedArr = this.copyDeep(oldList || []);
    return (newList || []).reduce(function (inAssembleArr, item) {
      // 查找拼装数组中需要更新的数据
      if (fixedArr.length > 0) {
        for (var i = 0; i < inAssembleArr.length; i++) {
          var b = inAssembleArr[i];
          if (item[primaryKey] == 0 || b[primaryKey] == 0) {
            continue;
          }
          // 主键值相同时才会有更新的数据产生
          if (item[primaryKey] == b[primaryKey]) {
            var isChange = false;
            for (var j = 0; j < compareArr.length; j++) {
              var compare = compareArr[j];
              if (item[compare] != b[compare]) {
                isChange = true;
                b[compare] = item[compare];
              }
            }
            if (isChange) {
              return inAssembleArr;
            }
            // 更新队列中删除没有做过变更的项
            inAssembleArr.splice(i, 1);
            return inAssembleArr;
          }
        }
      }
      inAssembleArr.unshift(item);
      return inAssembleArr;
    }, oldList || []);
  },
}
export {
  productUtil
}
