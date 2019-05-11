toastr.options = {
    "closeButton": true,
    "debug": false,
    "newestOnTop": false,
    "progressBar": true,
    "positionClass": "toast-bottom-left",
    "preventDuplicates": false,
    "onclick": null,
    "showDuration": "300",
    "hideDuration": "1000",
    "timeOut": "5000",
    "extendedTimeOut": "5000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
};

function ShowLoading(txt, style='success', title = '') {
    switch (style) {
        case "success":
            toastr.success(txt, title);
            break;
        case "info":
            toastr.info(txt, title);
            break;
        case "warning":
            toastr.warning(txt, title);
            break;
        case "error":
            toastr.error(txt, title);
            break;
    }
}
function getCookie(cname) {
    var name = cname + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var ca = decodedCookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}
function getCookieAuth(cname) {
    return "Bearer " + getCookie(cname);
}
Vue.component('select2', {
    props: ['options', 'value'],
    template: '<select style="width: 100%;"><slot></slot></select>',
    mounted: function () {
        var vm = this
        $(this.$el) 
            .select2({ data: this.options })
            .val(this.value)
            .trigger('change') 
            .on('change', function () {
                vm.$emit('input', this.value)
            })
    },
    watch: {
        value: function (value) { 
            $(this.$el)
                .val(value)
                .trigger('change')
        },
        options: function (options) { 
            $(this.$el).empty().select2({ data: options })
        }
    },
    destroyed: function () {
        $(this.$el).off().select2('destroy')
    }
});

Vue.component('cooltip', {
    template: '<span><span class="text-info" v-bind:title="tip" data-toggle="tooltip" v-bind:data-placement="position" v-bind:data-original-title="tip"><slot></slot></span></span>',
    props: ['tip', 'position'],
    methods: {},
    data: function () {
        return {
            message: "Tip" + this.tip,
        }
    },
});

Vue.component('datepicker', {
    props: ['value'],
    template: '<input v-bind:value="value">',
    mounted: function () {
        var vm = this
        $(this.$el)
            .datepicker({
                autoclose: true,
                todayHighlight: true,
                format: 'dd-M-yyyy', 
                todayBtn: "linked"
            })
            .val(this.value)
            .trigger('changeDate')
            .on('changeDate', function () {
                vm.$emit('input', this.value)
            })
    },
    watch: {
        value: function (value) {
            $(this.$el)
                .val(value) 
        } 
    },
    destroyed: function () {
        //$(this.$el).off().select2('destroy')
    }
});

Vue.component("step-navigation-step", {
    template: "#step-navigation-step-template",

    props: ["step", "currentstep"],

    computed: {
        indicatorclass() {
            return {
                active: this.step.id == this.currentstep,
                complete: this.currentstep > this.step.id
            };
        }
    }
});

Vue.component("step-navigation", {
    template: "#step-navigation-template",

    props: ["steps", "currentstep"]
});

Vue.component("step", {
    template: "#step-template",

    props: ["step", "stepcount", "currentstep", "canmove"],

    computed: {
        active() {
            return this.step.id == this.currentstep;
        },

        firststep() {
            return this.currentstep == 1;
        },

        laststep() {
            return this.currentstep == this.stepcount;
        },

        stepWrapperClass() {
            return {
                active: this.active
            };
        }
    },

    methods: {
        nextStep() {
            if (this.canmove) {
                this.$emit("step-change", this.currentstep + 1);
            } else {
                ShowLoading(this.errormsg, 'warning');
            }
        },

        lastStep() {
            this.$emit("step-change", this.currentstep - 1);
        }
    }
});
 
var FormUtil = (function () {
    var auth = getCookieAuth("usertoken");
    var baseurl = '';
    var action = '';
    var entity = '';
    var mdCreate = '';
    var mdUpdate = '';
    var mdDetail = '';
    var _query = "";
    var settings = {
        "processing": true,
        "serverSide": true,
        "ordering": false,
        "searching": false,
        "responsive": true,
        "pagingType": 'full_numbers',
        "language": {
            "paginate": {
                "previous": 'prev',
                "first": 'first',
                "last": 'last',
                "next": 'next'
            }
        },
        "dom": 'rt<"bottom"f<"row"<"col-md-4 small"l><"col-md-4 text-center small"i><"col-md-4"p>>><"clear">',
        "lengthMenu": [[10, 15, 25, 50, -1], [10, 15, 25, 50, "All"]],
        "ajax": function (data, callback, settings) {
            var n = location.search.substring(1);
            if (_query !== '') {
                if (n !== '') {
                    n = n + "&" + _query;
                }
                else {

                    n = _query;
                }
            } 
            var k = {
                q: 1
            };
            if (n !== "") {
                k = JSON.parse('{"' + decodeURI(n.replace(/&/g, "\",\"").replace(/=/g, "\":\"")) + '"}');
            }
             
            var i = data.start;
            var j = data.length;
            k.page = (i / j) + 1;
            k.draw = data.draw;
            k.pageSize = j;
            for (var property in k) {
                if (k.hasOwnProperty(property)) {
                    $("#" + property).val(k[property]);
                }
            }
            $.ajax({
                type: "GET",
                url: baseurl + "/api/" + entity + "/Search",
                async: true,
                data: k,
                headers: { "Authorization": auth },
                success: function (data, textStatus, XMLHttpRequest) {
                    var p = JSON.parse(XMLHttpRequest.getResponseHeader('X-Pagination'));

                    $("#searchInfo").html(p.Summary);
                    var json = {
                        draw: p.Draw,
                        recordsTotal: p.TotalCount,
                        recordsFiltered: p.TotalCount,
                        data: data
                    };
                    callback(json);
                },

            })
        },
    };
    var app = { obj: 1 };
    var filterapp = { obj: 1 };
    var innerform = { obj: 1 };
    var table = { obj: 1 };


    var _methods = function () {
        return {
            meth1: function () {
                alert("hi");
            }
        }
    };
    return {
        ShowLoading: function (msg, title) {
            ShowLoading(msg, title);
            return 1;
        },
        Init: function (args) {
            entity = args.entity;
            baseurl = args.url;
            innerform = args.form;
            settings.columns = args.columns;

            mdCreate = args.createModal;
            mdUpdate = args.updateModal;
            mdDetail = args.detailModal;

            if (args.query !== undefined) {
                _query = args.query;
            }
            
            table = $('#' + args.table).DataTable(settings);

            if (args.methods === undefined) {
                args.methods = _methods;
            }


            app = new Vue({
                el: '#appView',
                data: {
                    frmTitle: '',
                    form: innerform,
                    list: args.list
                },

                methods: {
                    submit: function () {
                        $.ajax({
                            type: "POST",
                            url: baseurl + "/api/" + entity + "/" + action + "?id=" + this.form.Id + "&" + _query,
                            data: this.form,
                            headers: { "Authorization": auth },
                            async: true,
                            success: function (data) {
                                if (action == "Create") {
                                    FormUtil.ShowLoading("Create Successful", "success");
                                    $('#' + mdCreate).modal('hide');
                                }

                                if (action == "Update") {
                                    FormUtil.ShowLoading("Update Successful", "success");
                                    $('#' + mdUpdate).modal('hide');
                                }
                                table.ajax.reload();
                            },
                            error: function (data) {
                                FormUtil.ShowLoading(data.statusText + ": " + data.responseJSON.Message, "error");
                            }
                        });
                    },
                    deleteItem: function (event) {
                        //console.log(JSON.stringify(this.form));

                        if (!confirm("Are you sure you want to delete?")) {
                            return 1;
                        }
                        $.ajax({
                            type: "POST",
                            url: baseurl + "/api/" + entity + "/Delete?id=" + this.form.Id + "&" + _query,
                            async: true,
                            headers: { "Authorization": auth },
                            success: function (data) {
                                FormUtil.ShowLoading("Delete Successful", "success");
                                table.ajax.reload();
                                $('#' + mdDetail).modal('hide');
                            },
                            error: function (data) {
                                FormUtil.ShowLoading(data.statusText + ": " + data.responseJSON.Message, "error");
                            }
                        });
                    },
                    page: args.methods
                }
            }); 
            filterapp = new Vue({
                el: '#filterView',
                data: {
                    list: args.list,
                    formsearch: args.formsearch
                }
            });
            return 1;
        },
        Grid: function (args) {
            entity = args.entity;
            baseurl = args.url; 
            settings.columns = args.columns;  
            if (args.query !== undefined) {
                _query = args.query;
            }
            if (args.row !== undefined) {
                settings.createdRow = args.row.createdRow;
            }
            table = $('#' + args.table).DataTable(settings); 
      
            return 1;
        },
        UpdateForm: function (args) {
            entity = args.entity;
            baseurl = args.url;
            innerform = args.form;
            action = "Update";
            app = new Vue({
                el: '#appView',
                data: {
                    frmTitle: '',
                    form: innerform,
                    list: args.list
                },

                methods: {
                    submit: function () {
                        $.ajax({
                            type: "POST",
                            url: baseurl + "/api/" + entity + "/" + action + "?id=" + this.form.Id + "&" + _query,
                            data: this.form,
                            async: true,
                            headers: { "Authorization": auth },
                            success: function (data) {
                                if (action == "Create") {
                                    FormUtil.ShowLoading("Create Successful", "success");
                                }
                                if (action == "Update") {
                                    FormUtil.ShowLoading("Update Successful", "success");
                                }
                            },
                            error: function (data) {
                                FormUtil.ShowLoading(data.statusText + ": " + data.responseJSON.Message, "error");
                            }
                        });
                    }
                }
            });
            return 1;
        },
        FormCreate: function () {
            $('#' + mdCreate).modal('show');
            action = 'Create';
            app.frmTitle = entity + ': Create';
            app.form = innerform;
            return 1;
        },
        FormUpdate: function (id) {
            $.ajax({
                type: "GET",
                url: baseurl + "/api/" + entity + "/detail?id=" + id + "&" + _query,
                async: true,
                headers: { "Authorization": auth },
                success: function (data) {
                    action = 'Update';
                    app.frmTitle = entity + ': Update';
                    app.form = data;
                    $('#' + mdUpdate).modal('show');
                },
                error: function (data) {
                    FormUtil.ShowLoading(data.statusText + ": " + data.responseJSON.Message, "error");
                }
            });
            return 1;
        },
        FormDetail: function (id) {
            $.ajax({
                type: "GET",
                url: baseurl + "/api/" + entity + "/detail?id=" + id + "&" + _query,
                async: true,
                headers: { "Authorization": auth },
                success: function (data) {
                    app.frmTitle = entity + ': Detail';
                    app.form = data;
                    $('#' + mdDetail).modal('show');
                },
                error: function (data) {
                    FormUtil.ShowLoading(data.statusText + ": " + data.responseJSON.Message, "error");
                }
            });
            return 1;
        },
        GetPersonByMobile: function (mobile) {
            var person = { id: 0 };
            $.ajax({
                type: "GET",
                url: baseurl + "/api/Persons/GetByMobile?Id=" + mobile,
                async: false,
                headers: { "Authorization": auth },
                success: function (data) {
                    person = data;
                },
                error: function (data) {
                    
                }
            });
            return person;
        }
    };

}());


var SiteUtil = (function () {
    var baseurl = ''; 
     
    return {
        LogOut: function () {
            location.href = _baseurl + "/Accounts/Logoff";
            return 1;
        },
        Today: function () {
            return moment().format('MMMM Do YYYY');
        },
        formatPrice: function (value) {
            let val = (value / 1).toFixed(2);
            return val.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
        } 
    };

}());