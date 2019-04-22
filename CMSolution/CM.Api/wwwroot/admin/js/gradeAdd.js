//JavaScript代码区域
layui.use(['element', 'form', 'layer'], function () {
    var element = layui.element;
    var $ = layui.jquery;
    var form = layui.form;
    var layer = layui.layer;

    $.extend(form,
        {
            formSave: function (isEdit, mdata) {
                var method = "POST";
                if (isEdit) method = "PUT";
                var myurl = "/api/stu";

                // $.extend(mdata,{isEdit
                //   token:"itg"
                // });

                $.ajax({
                    type: method,
                    accepts: "application/json",
                    url: myurl,
                    contentType: "application/json",
                    data: JSON.stringify(mdata),
                    success: function (data) {
                        if (data.code == 200) {
                            parent.layui.table.showAll();
                            var index = parent.layer.getFrameIndex(window.name); //先得到当前iframe层的索引
                            parent.layer.close(index); //再执行关闭   
                        } else {
                            layer.alert(data.msg);
                        }

                    }
                });
            }
        });

    var editFlag = false; //编辑标识
    //edit
    if (UrlParam.hasParam('edit')) { //当前url中是否包含edit关键字
        editFlag = true;
        $('cite').text('修改学生成绩');
        var editkey = UrlParam.paramValues('edit');
        var item = layui.data('edit')[editkey];
        layui.data('edit', {
            key: editkey,
            remove: true
        });

        if ($.isEmptyObject(item)) { //使用jquery判断是否为空对象
            layer.alert('有了回调', function (index) {
                //do something            
                layer.close(index);
            });
        } else {
            form.val('myform', item); //初始化表单的值
            $("input[name='sno']").attr('readonly', 'true');
        }
    }

    //监听提交
    form.on('submit(formDemo)', function (data) {
        form.formSave(editFlag, data.field);

        return false;

    });

});