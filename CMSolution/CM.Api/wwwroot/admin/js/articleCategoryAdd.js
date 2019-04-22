//JavaScript代码区域
layui.use(['element', 'dtree', 'form', 'layer'], function () {
    var element = layui.element;
    var $ = layui.jquery;
    var dtree = layui.dtree;
    var form = layui.form;
    var layer = layui.layer;


    //下拉列表
    var drp = $("#pid");
    $.extend(drp, {
        selectedItem: null,             //下拉框选项数据项
        setSelection: function (obj) {      //设置下拉框选中项
            if (obj.pid < 0) obj.pid = 0;
            drp.selectedItem = obj;
            $("#txtPid").val(obj.name);
        },
        drpInit: function () {
            var method = "GET";
            var myurl = "/api/category";
            var mdata;
            $.ajax({
                type: method,
                accepts: "application/json",
                url: myurl,
                contentType: "application/json",
                data: JSON.stringify(mdata),
                success: function (data) {
                    if (data.code == 200) {
                        data.data.unshift({
                            id: -1,
                            name: "<顶级>",
                            parentCategoryId: 0,
                            iconClass: "dtree-icon-xiangyou"
                        });

                        //初始化下拉列表数
                        dtree.render({
                            elem: "#slTree",
                            data: data.data,
                            dataFormat: "list",
                            response: {
                                title: "name",
                                parentId: "parentCategoryId"
                            },
                            ficon: "1",
                            initLevel: "3"
                        });

                        //绑定input点击事件
                        $("#txtPid").on("click", function () {
                            $(this).toggleClass("layui-form-selected");
                            $("#test").toggleClass("layui-show layui-anim layui-anim-upbit");
                        });

                        //绑定树节点点击事件
                        dtree.on("node(slTree)", function (obj) {
                            drp.setSelection({
                                pid: obj.param.nodeId,
                                name: obj.param.context

                            });
                            $("#txtpid").toggleClass("layui-form-selected");
                            $("#test").toggleClass("layui-show layui-anim layui-anim-upbit");
                        });
                    } else {
                        layer.alert(data.msg);
                    }
                }
            });
        }
    });

    $.extend(form,
        {
            editFlag: false,
            oldItem: {},
            init: function() {
                drp.drpInit();
                form.formInit();
            },
            formInit: function () {
                if (UrlParam.hasParam('edit')) {
                    form.editFlag = true;
                    $('cite').text('修改栏目档案');
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
                        form.oldItem = item;
                        form.val('myform', item);

                        var pobj = {
                            pid: item.pid,
                            name: null
                        };
                        if (pobj.pid == 0) {
                            pobk.name = "<顶级>";
                            drp.setSelection(pobj);

                        if (!pobj.name && pobj.pid >0) {
                                var method = "GET";
                                var myurl = "/api/category/" + pobj.pid;
                                var mdata;

                            $.ajax({
                                type: method,
                                accepts: "application/json",
                                url: myurl,
                                contentType: "application/json",
                                data: JSON.stringify(mdata),
                                success: function (data) {
                                    if (data.code == 200) {
                                        if (data.data.length > 0) {
                                            pobj.name = data.data[0].name;
                                        } else {
                                            pobj.pid = 0;
                                            pobj.name = "<顶级>";
                                        }
                                        drp.setSelection(pobj);
                                    } else {
                                        layer.alert(data.msg);
                                    }
                                }
                            });
                           }
                        }
                    }
                }
                else
                    $("input[name='Sort']").val("99");

                if(UrlParam.hasParam('add')){
                $('cite').text('添加下级栏目');
                var key = UrlParam.paramValues('add');
                var item = layui.data('add')[key];
                layui.data('add', {
                    key: key,
                    remove: true
                });
                if ($.isEmptyObject(item) ){
                    layer.alert('有了回调', function (index) {
                        layer.close(index);
                });
                } else {
                    var pobj = {
                        pid: item.id,
                        name: item.Name
                    };
                    drp.setSelection(pobj);
                    }   
                }

                //监听提交
                form.on('submit(formDemo)', function (data) {
                    var upData = {
                        ParentCategoryId: drp.selectedItem.pid,
                        Name: data.field.Name,
                        DisplaySequence: data.field.Sort
                    };
                    if (form.editFlag) {
                        $.extend(upData, {
                            Id: form.oldItem.id
                        });
                    }
                    form.formSave(upData);
                    return false;
                })

                
            },
            formSave: function (mdata) {
                var method = "POST";
                var myurl = "/api/category";
                if (form.editFlag) {
                    method = "PUT";
                    myurl = "/api/category/" + mdata.Id;
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
                            parent.layui.treeTable.showAll();
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

    

});