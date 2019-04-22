//JavaScript代码区域
layui.use(['element', 'form', 'layer','dtree'], function () {
    var element = layui.element;
    var $ = layui.jquery;
    var form = layui.form;
    var layer = layui.layer;
    var dtree = layui.dtree;

    var drp = $("#role");
    $.extend(drp, {
        selectedItem: null,
        setSelection: function (obj) {
            if (obj.pid < 0) obj.pid = 0;
            drp.selectedItem = obj;
            $("#roleName").val(obj.name);
        },
        drpInit: function () {
            var method = "GET";
            var myurl = "/api/role";
            var mdata;
            $.ajax({
                type: method,
                accepts: "application/json",
                url: myurl,
                contentType: "application/json",
                data: JSON.stringify(myurl),
                success: function (data) {
                    if (data.code == 200) {
                        menu = [];
                        data.data.forEach(function (item) {
                            menu.push($.extend(item, { pid: 0 }));
                        });
                        dtree.render({
                            elem: "#slTree",
                            data: menu,
                            dataFormat: "list",
                            response: {
                                title: "name",
                                parentId: "pid"
                            },
                            ficon: "1",
                            initLevel: "3"
                        });
                        $("#roleName").on("click", function () {
                            $(this).toggleClass("layui-form-selected");
                            $("#test").toggleClass("layui-show layui-anim layui-anim-upbit");
                        });

                        dtree.on("node(slTree)", function (obj) {
                            drp.setSelection({
                                pid: obj.param.nodeId,
                                name: obj.param.context
                            });
                            $("#roleName").toggleClass("layui-form-selected");
                            $("#test").toggleClass("layui-show layui-anim layui-anim-upbit");
                        });
                        } else {
                        layui.alert(data.msg);
                    }
                }
            });

        },
    });

    $.extend(form,
        {
            editFlag: false,
            oldItem: {},
            init: function () {
                drp.drpInit();
                form.formInit();
            },
            formInit: function () {
                if (UrlParam.hasParam('edit')) {
                    form.editFlag = true;
                    $('cite').text('修改人员信息');
                    var editkey = UrlParam.paramValues('edit');
                    var item = layui.data('edit')[editkey];
                    layui.data('edit', {
                        key: editkey,
                        remove: true
                    });

                    if ($.isEmptyObject(item)) {
                        layer.alert('有了回调', function (index) {
                            layer.close(index);
                        });
                    } else {
                        drp.setSelection({
                            pid: item.roleId,
                            name: item.roleName
                        });
                        form.oldItem = item;
                        form.val('myform', item);
                    }

                }
                else
                    $("input[name='Sort']").val("99");

                form.on('submit(formDemo)', function (data) {
                    var upData = {
                        userName: data.field.userName,
                        realName: data.field.realName,
                        roleId: drp.selectedItem.pid,
                        mobile: data.field.mobile,
                        remark: data.field.remark
                    };
                    if (form.editFlag) {
                        $.extend(upData, {
                            Id: form.oldItem.id
                        });
                    }

                    form.formSave(upData);
                    return false;

                });

            },
            formSave: function (mdata) {
                var method = "POST";
                
                var myurl = "/api/manager";
                if (form.editFlag) {
                    method = "PUT";
                    myurl = "/api/manager/" + mdata.Id;
                }
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
                            parent.layui.page.render();
                            var index = parent.layer.getFrameIndex(window.name); //先得到当前iframe层的索引
                            parent.layer.close(index); //再执行关闭   
                        } else {
                            layer.alert(data.msg);
                        }

                    }
                });
            }
        });

    form.init();

    //var editFlag = false; //编辑标识
    ////edit
    //if (UrlParam.hasParam('edit')) { //当前url中是否包含edit关键字
    //    editFlag = true;
    //    $('cite').text('修改学生成绩');
    //    var editkey = UrlParam.paramValues('edit');
    //    var item = layui.data('edit')[editkey];
    //    layui.data('edit', {
    //        key: editkey,
    //        remove: true
    //    });

    //    if ($.isEmptyObject(item)) { //使用jquery判断是否为空对象
    //        layer.alert('有了回调', function (index) {
    //            //do something            
    //            layer.close(index);
    //        });
    //    } else {
    //        form.val('myform', item); //初始化表单的值
    //        $("input[name='sno']").attr('readonly', 'true');
    //    }
    //}

    ////监听提交
    //form.on('submit(formDemo)', function (data) {
    //    form.formSave(editFlag, data.field);

    //    return false;

    //});

});