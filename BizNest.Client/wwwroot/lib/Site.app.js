var _auth = getCookieAuth("usertoken");

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
        },
        formatDate: function (value, typ) {
            let val = moment(value);
            var t = "";
            switch (typ) {
                case 1:
                    t = val.format("ll");
                    break;
            }
            return t;
        },
        formatLabel: function (value) { 
            var t = "";
            switch (value) {
                case 'DR':
                    t = 'label-danger';
                    break;
                case 'CR':
                    t = 'label-success';
                    break;
            }
            return t;
        },
        IsValidAmount: function (amt) {
            var n = new Number(amt);
            if (isNaN(n)) {  
                return false;
            }
            if (n < 0.01) { 
                return false;
            } 
            return true;
        }
    };
}());

var DataUtil = (function () {
    var baseapi = _baseapi;
    return {

        SearchName: function (id) {  
            return $.ajax({
                type: "GET",
                url: baseapi + "/api/Search?query=" + id,
                async: true,
                contentType: 'application/json'
            });
        },
        
        Today: function () {
            return moment().format('MMMM Do YYYY');
        }
    };
}());
 