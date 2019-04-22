layui.use(['element', 'treeTable', 'form', 'layer'], function () {
    var $ = layui.jquery;
    var element = layui.element;
    var table = layui.treeTable;
    var layer = layui.layer;
    var form = layui.form;

    $.extend(table, {
        init: function () {
            table.on('tree(addSub)', function (obj) {
                table.addSub(obj.item);
            });

            table.on('tree(rowEdit)', function (obj) {
                table.rowEdit(obj.item);
            });

            table.on('tree(rowDel)', function (obj) {
                table.rowDel(obj.item);
            });

            $("#btnAdd").on("click", function () {
                table.add();
            })

            $("#search").on("click", function () {
                table.search();
            })

        },
        showAll: function () {
            var index = layer.load(2);
            $.ajax({
                type: "GET",
                url: "/api/category",
                //url: "/json/role.json",
                data: {
                    token: "itg"
                },
                dataType: "json",
                success: function (data) {
                    table.show(data);
                    layer.close(index);
                },
                error: function (XMLHttpRequest, TextStatus, errorThrown) {
                    layer.close(index);
                }
            });
        },
        show: function (mydata) {
            var dataList = [];
            mydata.data.forEach(item => {
                dataList.push({
                    "id": item.id,
                    "Name": item.name,
                    "pid": item.parentCategoryId,
                    "Sort":item.displaySequence
                })
            });
            table.render({
                elem: '#tree-table',
                data: dataList,
                icon_key: 'Name',
                is_checkbox: false,
                end: function (e) {
                    form.render();
                },
                cols: [
                    {
                        key: 'id',
                        title: 'ID',
                        width: '50px',
                        align: 'center',
                    },
                    {
                        key: 'Name',
                        title: '栏目名称',
                        template: function (item) {
                            if (item.level == 0 && (!item.is_end)) {
                                return '<span style="font-weight:bold;">' + item.Name + '</span>';
                            } else {
                                return '<span>' + item.Name + '</span>';
                            } 
                        }
                    },
                    
                    {
                        key: 'Sort',
                        title: '排序号',
                        width: '50px',
                        align: 'center',
                    },
                    {
                        title: '操作',
                        align: 'center',
                        width: '250px',
                        template: function (item) {
                            var tem = [];
                            tem.push('<a class="layui-btn layui-btn-danger layui-btn-xs" lay-filter="rowDel"><i class= "layui-icon layui-icon-delete"></i>删除</a>');
                            tem.push('<a class="layui-btn layui-btn-normal layui-btn-xs" lay-filter="rowEdit"><i class= "layui-icon layui-icon-edit"></i>编辑</a>');
                            tem.push('<a class="layui-btn layui-btn-xs" lay-filter="addSub"><i class= "layui-icon layui-icon-add-1"></i>子类</a>');
                            return tem.join(' ');
                        }
                    }
                ]
            });

        },
        add: function (obj) {
            layer.open({
                type: 2 //此处以iframe举例
                ,
                title: '文章栏目',
                area: ['720px', '500px'],
                maxmin: true,
                content: ['articleCateAdd.html', 'no']
            });
        },
        addSub: function (data) {
            var tb = "add";
            var fkey = tb + '_category_' + data.id;
            layui.data(tb, {
                key: fkey,
                value: data
            });
            layer.open({
                type: 2,
                title: '添加下级栏目',
                area: ['720px', '500px'],
                shade: 0,
                maxmin: true,
                content: ['articleCateAdd.html?add=' + fkey, 'no']
            });
        },
        adv: function (obj) {
            alert('高级搜索');
        },
        delAll: function (obj) {
            alert('delAll');
        },
        search: function (obj) {
            //alert('reload');
            var myurl = "/api/Category";
            var key = $("#demoReload").val();
            if (key != "") {
                myurl = myurl + "/" + key;
            }

            $.ajax({
                type: "GET",
                url: myurl,
                data: {
                    token: "itg"
                },
                dataType: "json",
                success: function (data) {
                    table.show(data);
                }
            });

        },
        rowDel: function (data) {
            console.log(data);
            layer.confirm('您确定要删除?', {
                icon: 3,
                title: '提示'
            }, function (index) {
                //do something
                $.ajax({
                    url: "/api/category/" + data.id,
                    type: "DELETE",
                    success: function (result) {
                        table.showAll();
                    }
                });
                layer.close(index);
            });
        },
        rowEdit: function (data) {
            //alert('rowEdit');
            // console.log(data);
            var tb = "edit";
            var fkey = tb + '_category_' + data.id;
            layui.data(tb, {
                key: fkey,
                value: data
            });
            layer.open({
                type: 2, //此处以iframe举例   
                title: '编辑学生成绩',
                area: ['720px', '500px'],
                shade: 0,
                maxmin: true,
                content: ['articleCateAdd.html?edit=' + fkey, 'no']
            });

        }
    });


    table.init();
    table.showAll();


});