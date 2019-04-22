layui.use(['element', 'table', 'layer'], function () {
    var $ = layui.jquery;
    var element = layui.element;
    var table = layui.table;
    var layer = layui.layer;

    $.extend(table, {
        showAll: function () {
            $.ajax({
                type: "GET",
                url: "/api/Role",
                //url: "/json/role.json",
                data: {
                    token: "itg"
                },
                dataType: "json",
                success: function (data) {
                    table.show(data);
                }
            });
        },
        show: function (mydata) {
            table.render({
                elem: '#tab',
                toolbar: '#mytoolbar',
                skin: 'line',
                data: mydata.data,
                cols: [
                    [{
                        type: 'checkbox'
                    }, {
                        field: 'id',
                        title: 'ID',
                        sort: true
                    }, {
                        field: 'name',
                        title: '角色名称',
                        sort: true
                    }, {
                        title: '操作',
                        fixed: 'right',
                        width: 178,
                        align: 'center',
                        toolbar: '#barDemo'
                    }]
                ],
                id: 'testReload',
                page: {
                    count: 10,
                    limit: 10,
                    limits: [10, 20, 30, 50],
                    layout: ['count', 'prev', 'page', 'next', 'limit', 'refresh', 'skip'],
                    jump: function (obj) {
                        console.log(obj)
                    }
                }
            });

        },
        add: function (obj) {
            layer.open({
                type: 2 //此处以iframe举例
                ,
                title: '添加学生成绩',
                area: ['720px', '500px'],
                maxmin: true,
                content: ['gradeAdd.html', 'no']
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
            var myurl = "/api/Role";
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
                    url: "/api/Role/" + data.id,
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
            var fkey = tb + '_stu_' + data.id;
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
                content: ['gradeAdd.html?edit=' + fkey, 'no']
            });

        }
    });

    //监听工具栏事件
    table.on('toolbar(tab)', function (obj) {
        table[obj.event] && table[obj.event](obj.data);
        // alert(obj.event);
        // console.log(obj);

    });
    //监听行工具栏事件
    table.on('tool(tab)', function (obj) {
        table[obj.event] && table[obj.event](obj.data);
        // alert(obj.event);
        // console.log(obj);
    });

    table.showAll();


});