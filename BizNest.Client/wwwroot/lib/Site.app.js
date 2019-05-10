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
        //Get product detail promise
        ProductPreview: function (id) {
            return $.ajax({
                type: "GET",
                url: baseapi + "/api/gogzee/Products/Detail?id=" + id,
                async: true,
                headers: { "Authorization": _auth }
            });
        },
        //Get product detail images promise
        ProductImages: function (id) {
            return $.ajax({
                type: "GET",
                url: baseapi + "/api/gogzee/Photos/Search?pictureTypeId=3&entityId=" + id,
                async: true,
                headers: { "Authorization": _auth }
            });
        },
        AddToCart: function (productId, quantity, itemnotes) {
            var data = {
                "SessionId": _session,
                "ProductId": productId,
                "Quantity": quantity,
                "ItemNotes": itemnotes
            };

            return $.ajax({
                type: "POST",
                url: baseapi + "/api/gogzee/OrderDetails/Create",
                async: true,
                data: data,
                headers: { "Authorization": _auth }
            });
        },
        GetCartSummary: function () {
            return $.ajax({
                type: "GET",
                url: baseapi + "/api/gogzee/OrderDetails/Summary?sessionId=" + _session,
                async: true,
                headers: { "Authorization": _auth }
            });
        },
        Today: function () {
            return moment().format('MMMM Do YYYY');
        }
    };
}());

var MerchantDataUtil = (function () {
    var baseapi = _baseapi; 
    return {
        //Get corporates for a logged on user
        UserCorporates: function (id) {
            return $.ajax({
                type: "GET",
                url: baseapi + "/api/Account/Corporates/UserCorporates?id=" + id,
                async: true,
                headers: { "Authorization": _auth }
            });
        },
        //gets all users in a corporate
        CorporateUsers: function () {
            return $.ajax({
                type: "GET",
                url: baseapi + "/api/Account/Corporates/UserCorporates?id=" + id,
                async: true,
                headers: { "Authorization": _auth }
            });
        },
        //Gets all merchants yet to be approved
        PendingMerchants: function () {
            return $.ajax({
                type: "GET",
                url: baseapi + "/api/Account/Corporates/Search?CorporateStatus=1",
                async: true,
                headers: { "Authorization": _auth }
            });
        },
        //Update a merchants detail
        UpdateMerchant: function (data) {
            return $.ajax({
                type: "POST",
                url: baseapi + "/api/Account/Corporates/Update?id=" + data.Id,
                async: true,
                data: data,
                headers: { "Authorization": _auth }
            });
        },
        //Approve a merchant for live
        ApproveMerchant: function (data) {
            return $.ajax({
                type: "POST",
                url: baseapi + "/api/Account/Corporates/Approve",
                async: true,
                data: data,
                headers: { "Authorization": _auth }
            });
        }, 
        //get users active corporates
        UserActiveCorporates: function (id) {
            return $.ajax({
                type: "GET",
                url: baseapi + "/api/Account/Corporates/UserCorporates?CorporateStatus=2&id=" + id,
                async: true,
                headers: { "Authorization": _auth }
            });
        },
        //get all wallets belonging to a merchant
        MerchantWallets: function (id) {
            return $.ajax({
                type: "GET",
                url: baseapi + "/api/MyChange/Wallets/Merchant?id=" + id,
                async: true,
                headers: { "Authorization": _auth }
            });
        },

        MerchantStatement: function (id) {
            return $.ajax({
                type: "GET",
                url: baseapi + "/api/MyChange/WalletStatements/Merchant?id=" + id + "&fields=Amount,CurrentBalance,CurrentBalanceBF,CurrencyName,Narration,ValueDate,DrCrText",
                async: true,
                headers: { "Authorization": _auth }
            });
        },
        MerchantSearch: function (id) {
            return $.ajax({
                type: "GET",
                url: baseapi + "/api/MyChange/Corporates/SearchTags?qry=" + id,
                async: true
            });
        },
        Today: function () {
            return moment().format('MMMM Do YYYY');
        }
    };
}());

var TransactDataUtil = (function () {
    var baseapi = _baseapi;
    return {
        //Get product detail promise
        InitiateFund: function (transact) {
            return $.ajax({
                type: "POST",
                url: baseapi + "/api/MyChange/WalletTransacts/InitiateFund",
                async: false,
                data: transact,
                headers: { "Authorization": _auth }
            });
        },
        CancelPayment: function (id) {
            return $.ajax({
                type: "GET",
                url: baseapi + "/api/MyChange/WalletTransacts/UserCancel?id=" + id,
                async: false, 
                headers: { "Authorization": _auth }
            });
        }, 
        ForwardPayment: function (transact) {
            return $.ajax({
                type: "POST",
                url: baseapi + "/api/MyChange/WalletTransacts/ForwardPayment",
                async: false,
                data: transact,
                headers: { "Authorization": _auth }
            }); 
        },
        Revalidate: function (transact) {
            return $.ajax({
                type: "POST",
                url: baseapi + "/api/MyChange/WalletTransacts/Revalidate",
                async: true,
                data: transact,
                headers: { "Authorization": _auth }
            });
        },
        CallGateway: function (transact, mode) { 
            getpaidSetup({
                PBFPubKey: transact.PublicKey,
                customer_email: transact.Email, 
                custom_description: mode, 
                custom_title: "myChange",
                amount: transact.Amount,
                customer_phone: '',
                country: "NG",
                currency: "NGN",
                txref: transact.TxId, 
                onclose: function () {
                    var req = TransactDataUtil.CancelPayment(transact.TxId);
                    req.done(function (data) {
                        window.location = _baseurl + '/Transactions/Revalidate/' + transact.TxId;  
                    });
                    req.fail(function (data) {
                        ShowLoading("could not cancel transaction", "error");
                    });
                    return 1;
                },
                callback: function (response) {
                    console.log(JSON.stringify(response));
                    transact.GatewayRef = response.tx.flwRef;  
                    var req = TransactDataUtil.Revalidate(transact);
                    req.done(function (data) {
                        window.location = _baseurl + '/Transactions/Revalidate/' + transact.TxId;  
                    });
                    req.fail(function (data) {
                        window.location = _baseurl + '/Transactions/Revalidate/' + transact.TxId;  
                    });   
                }
            }); 
        },
        GiveChange: function (transact) {
            return $.ajax({
                type: "POST",
                url: baseapi + "/api/MyChange/WalletTransacts/GiveChange",
                async: true,
                data: transact,
                headers: { "Authorization": _auth }
            });
        },
        Today: function () {
            return moment().format('MMMM Do YYYY');
        }
    };
}());

var UserDataUtil = (function () {
    var baseapi = _baseapi;
    return {
        //Get user details by mobile
        LookUpMobile: function (mobile) {
            return $.ajax({
                type: "GET",
                url: baseapi + "/api/Account/SiteUsers/Mobile?id=" + mobile, 
                async: true,
                headers: { "Authorization": _auth }
            });
        },
        DashboardHome: function () {
            return $.ajax({
                type: "GET",
                url: baseapi + "/api/MyChange/Dashboard/Home",
                async: true
            });
        }, 
        UserStatement: function (id) {
            return $.ajax({
                type: "GET",
                url: baseapi + "/api/MyChange/WalletStatements/Subscriber?id=" + id + "&fields=Amount,CurrentBalance,CurrentBalanceBF,CurrencyName,Narration,ValueDate,DrCrText",
                async: true,
                headers: { "Authorization": _auth }
            });
        },
        Today: function () {
            return moment().format('MMMM Do YYYY');
        }
    };
}());