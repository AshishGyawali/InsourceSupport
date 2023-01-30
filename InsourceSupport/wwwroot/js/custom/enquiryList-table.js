$(() => {

    const now = new Date();
    var oneMonthAgo = new Date(
        new Date().getFullYear(),
        new Date().getMonth() - 1,
        new Date().getDate()
    );

    $("#dateFrom").dxDateBox({
        applyValueMode: 'useButtons',
        name: 'dateFrom',
        type: "date",
        value: oneMonthAgo,
        max: now,
        min: new Date(1900, 0, 1),
        label: "Date from",
        labelMode: "floating",
    });
    $("#dateTo").dxDateBox({
        applyValueMode: 'useButtons',
        name: 'dateTo',
        type: "date",
        value: now,
        max: now,
        min: new Date(1900, 0, 1),
        label: "Date To",
        labelMode: "floating",
    });
    $('#softwareName').dxSelectBox({
        dataSource: [],
        showClearButton: true,
        name: 'softwareId',
        displayExpr: 'Name',
        valueExpr: 'Id',
        label: "Software Name",
        labelMode: "static",
        onValueChanged(data) {
            $.ajax({
                type: 'GET',
                url: "/admin/home/getModuleList",
                data: {
                    softwareId: data.value
                },
                success: function (resultData) {
                    $('#moduleName').dxSelectBox('instance').option('dataSource', resultData)
                },
                error: function (resultData) { alert(resultData.Message) }
            });
        },
    });
    $('#moduleName').dxSelectBox({
        dataSource: [],
        name: 'moduleId',
        displayExpr: 'Name',
        showClearButton: true,
        valueExpr: 'Id',
        label: "Module Name",
        labelMode: "static",
    });
    $('#status').dxSelectBox({
        dataSource: [],
        showClearButton: true,
        name: 'statusId',
        displayExpr: 'Name',
        valueExpr: 'Id',
        label: "Status",
        labelMode: "static",
    });
    $("#searchButton").dxButton({
        name: 'searchButton',
        stylingMode: 'contained',
        icon: 'find',
        text: 'Search',
        type: 'default',
        width: 100,
        onClick() {
            var values = $('#enquirySearchKey').serialize();
            $('#gridContainer').dxDataGrid('instance').option('dataSource', "/admin/home/showEnquiryList?" + values)
        },
    });
    $.ajax({
        type: 'GET',
        url: "/admin/home/getSoftwareList",
        success: function (resultData) {
            $('#softwareName').dxSelectBox('instance').option('dataSource', resultData)
        },
        error: function (resultData) { alert(resultData.Message) }
    });
    $.ajax({
        type: 'GET',
        url: "/admin/home/getStatusList",
        success: function (resultData) {
            $('#status').dxSelectBox('instance').option('dataSource', resultData)
        },
        error: function (resultData) { alert(resultData.Message) }
    });

    $('#gridContainer').dxDataGrid({
        dataSource: "/admin/home/showEnquiryList/",
        paging: {
            pageSize: 10,
        },
        pager: {
            showPageSizeSelector: true,
            allowedPageSizes: [10, 25, 50, 100],
        },
        remoteOperations: false,
        searchPanel: {
            visible: true,
            highlightCaseSensitive: true,
        },
        groupPanel: { visible: true },
        grouping: {
            autoExpandAll: false,
        },
        allowColumnReordering: true,
        rowAlternationEnabled: false,
        showBorders: true,
        columns: [
            {
                dataField: 'SoftwareName',
                caption: 'Software'
            },
            {
                dataField: 'ModuleName',
                caption: 'Module',
                dataType: 'string',
            },
            {
                dataField: 'UserName',
                caption: 'Username',
                dataType: 'string',
            },
            {
                dataField: 'FullName',
                caption: 'Full Name',
                dataType: 'string',
            },
            {
                dataField: 'Email',
                dataType: 'string',
            },
            {
                dataField: 'ContactNumber',
                caption: 'Contact Number',
                dataType: 'string',
            },
            {
                dataField: 'Issue',
                dataType: 'string',
                cellTemplate: function (container, options) {
                    let txt = $("<a href='javascript:void(0)' />").text("Details");
                    txt.click(function (e) {
                        e.preventDefault();
                        if ($("#issuepopup").length == 0) {

                            $("<div />").attr("id", "issuepopup").appendTo(container)
                        }
                        let div = $("#issuepopup");
                        const popup = div.dxPopup({
                            contentTemplate: function (c, o) {
                                var files = options.data.Files;
                                var files = options.data.Files.split(',');
                                var images = '';
                                for (var i = 0; i < files.length; i++) {
                                    images += `<img src="/images/IssuesScreenshot/${files[i]}" class="thumbnail">`
                                }
                                var iconLogo = '';
                                if (options.data.TokenStatus == "Resolved") {
                                    iconLogo = `<i class="fa-solid fa-3x fa-check" style="color:#32af32;"></i><h4 style="color:#32af32;">Resolved</h4>`
                                }
                                if (options.data.TokenStatus == "UnderProcess") {
                                    iconLogo = `<img src="/images/underCons.png"><h4 style="color:#337ab7;">Processing</h4>`
                                }
                                if (options.data.TokenStatus == "Rejected") {
                                    iconLogo = `<i class="fa-solid fa-3x fa-xmark" style="color:#d9534f;"></i><h4 style="color:#d9534f;">Rejected</h4>`
                                }
                                if (options.data.TokenStatus == "Ambigious") {
                                    iconLogo = `<i class="fa-solid fa-3x fa-circle-exclamation"style="color:#ffc107;"></i><h4 style="color:#ffc107;">Ambigious</h4>`
                                }
                                if (options.data.TokenStatus == "Created") {
                                    iconLogo = `<i class="fa-solid fa-3x fa-bell"></i><h4>Created</h4>`
                                }
                                const statusIcon = $(`<div class="row">
                                                        <div class="col-md-10"></div>
                                                        <div class ="col-md-2" style="text-align:center;">
                                                            ${iconLogo}
                                                        </div>
                                                      </div>`);
                                c.append(statusIcon);
                                
                                const scrollView = $('<div />').attr("class", "issueContent");
                                scrollView.append($(`
                                                    <div class="col-md-12" style="text-align: justify;">
                                                       ${options.value} 
                                                    </div>
                                                    <div class="issueImages">
                                                        ${images}
                                                    </div>
                                `));
                                scrollView.dxScrollView({
                                    width: '85%',
                                    height: '100%',
                                });
                                return scrollView; 
                            },
                            onShown: function (e) {
                                 $('.issueImages').yzhanImageViewer({
                                    selector: '.thumbnail',
                                    attrSelector: 'src',
                                    parentSelector: 'div'
                                });
                            },
                            width: 1250,
                            height: 800,
                            showTitle: true,
                            title: 'Issue by ' + options.data.FullName + ' on ' + options.data.SoftwareName + '\'s ' + options.data.ModuleName + ' module',
                            visible: true,
                            dragEnabled: false,
                            hideOnOutsideClick: true,
                            showCloseButton: false,
                            toolbarItems: [{
                                locateInMenu: 'always',
                                widget: 'dxButton',
                                toolbar: 'top',
                                options: {
                                    text: 'View Attachments',
                                    onClick() {

                                    },
                                },
                            },
                            
                                {
                                widget: 'dxButton',
                                toolbar: 'bottom',
                                location: 'before',
                                options: {
                                    icon: 'todo',
                                    visible: options.data.TokenStatus == "Created" || options.data.TokenStatus == "Ambigious",
                                    text: 'Proceed',
                                    stylingMode: 'contained',
                                    type: 'default',
                                    width: 150,
                                    onClick() {
                                        $.ajax({
                                            type: 'POST',
                                            url: "/admin/home/ProcessIssue",
                                            data: {
                                                Id: options.data.Id
                                            },
                                            success: function () {
                                                $('#gridContainer').dxDataGrid('instance').refresh();
                                                const message = `Issue on ${options.data.SoftwareName}'s ${options.data.ModuleName} module by ${options.data.FullName} started processing.`;
                                                DevExpress.ui.notify({
                                                    message,
                                                    width: 300,
                                                    position: {
                                                        my: 'right top',
                                                        at: 'right top',
                                                    },
                                                }, 'success', 8000);
                                            },
                                            error: function () {
                                                const message = `Something went wrong`;
                                                DevExpress.ui.notify({
                                                    message,
                                                    width: 300,
                                                    position: {
                                                        my: 'right top',
                                                        at: 'right top',
                                                    },
                                                }, 'error', 3000);
                                            }
                                        });

                                    },
                                },
                                }, {
                                    widget: 'dxButton',
                                    toolbar: 'bottom',
                                    location: 'before',
                                    options: {
                                        text: 'Resolve',
                                        icon: 'check',
                                        visible: options.data.TokenStatus == 'UnderProcess',
                                        stylingMode: 'contained',
                                        type: 'success',
                                        width: 150,
                                        onClick() {
                                            popup.hide();
                                        },
                                    },
                                }, {
                                    widget: 'dxButton',
                                    toolbar: 'bottom',
                                    location: 'before',
                                    options: {
                                        text: 'Set Ambigious',
                                        cssClass:"amg",
                                        icon: 'fa-solid fa-exclamation',
                                        visible: options.data.TokenStatus == "Created" || options.data.TokenStatus == "UnderProcess",
                                        stylingMode: 'contained',
                                        
                                        type: 'warning',
                                        width: 155,
                                        onClick() {
                                            popup.hide();
                                        },
                                    },
                                },{
                                    widget: 'dxButton',
                                    toolbar: 'bottom',
                                    location: 'before',
                                    options: {
                                        text: 'Reject',
                                        icon: 'remove',
                                        visible: (options.data.TokenStatus != 'Resolved' && options.data.TokenStatus != "Rejected"),
                                        stylingMode: 'contained',
                                        type: 'danger',
                                        width: 150,
                                        onClick() {
                                            $.ajax({
                                                type: 'POST',
                                                url: "/admin/home/RejectIssue",
                                                data: {
                                                    Id: options.data.Id
                                                },
                                                success: function () {
                                                    $('#gridContainer').dxDataGrid('instance').refresh();
                                                    const message = `Issue on ${options.data.SoftwareName}'s ${options.data.ModuleName} module by ${options.data.FullName} has been rejected`;
                                                    DevExpress.ui.notify({
                                                        message,
                                                        width: 300,
                                                        position: {
                                                            my: 'right top',
                                                            at: 'right top',
                                                        },
                                                    }, 'success', 8000);
                                                },
                                                error: function () {
                                                    const message = `Something went wrong`;
                                                    DevExpress.ui.notify({
                                                        message,
                                                        width: 300,
                                                        position: {
                                                            my: 'right top',
                                                            at: 'right top',
                                                        },
                                                    }, 'error', 3000);
                                                }
                                            });
                                        },
                                    },
                                },{
                                    widget: 'dxButton',
                                    toolbar: 'bottom',
                                    location: 'before',
                                    options: {
                                        text: 'Transfer',
                                        visible: (options.data.TokenStatus == 'Created' || options.data.TokenStatus == "UnderProcess"),
                                        icon: 'fa-solid fa-share',
                                        stylingMode: 'contained',
                                        type: 'default',
                                        width: 150,
                                        onClick() {
                                            popup.hide();
                                        },
                                    },
                                },{
                                widget: 'dxButton',
                                toolbar: 'bottom',
                                location: 'after',
                                options: {
                                    text: 'Close',
                                    icon: 'fa-solid fa-circle-xmark',
                                    stylingMode: 'outlined',
                                    type: 'danger',
                                    width: 150,
                                    onClick() {
                                        popup.hide();
                                    },
                                },
                            }],
                        }).dxPopup('instance');
                        popup.show();
                    })
                    container.append(txt);
                }
            },
            {
                dataField: 'Token',
                caption: 'Token',
                dataType: 'string',
            },
            {
                dataField: 'TokenStatus',
                caption: 'Token Status',
                dataType: 'string',
            },
        ],
        onRowPrepared: function (info) {
            if (info.rowType == 'data') {
                if (info.data.TokenStatus == "UnderProcess") {
                    info.rowElement.addClass('under-process')
                }
                if (info.data.TokenStatus == "Resolved") {
                    info.rowElement.addClass('resolved')
                }
                if (info.data.TokenStatus == "Rejected") {
                    info.rowElement.addClass('rejected')
                }
                if (info.data.TokenStatus == "Ambigious") {
                    info.rowElement.addClass('ambigious')
                }
            }
        }
    });
})


!function (f) { var u = f("<div>").addClass("yz-img-viewer"), d = f("<div>").addClass("yz-img-viewer-bg"), m = f("<div>").addClass("yz-img-list").appendTo(u), g = f("<div>").addClass("yz-dot-indicator").appendTo(u), p = f("<div>").addClass("yz-debug").appendTo(u), w = f("<div>").addClass("yz-img-switch yz-img-switch-prev").appendTo(u), v = f("<div>").addClass("yz-img-switch yz-img-switch-next").appendTo(u), y = null, x = "", n = {}; function C(e, n) { n = "client" + n.toUpperCase(); return (e[n] || function (e) { var n = { clientX: 0, clientY: 0 }, t = e && e.length || 0; if (0 < t) { for (var i = 0; i < t; i++)n.clientX += e[i].clientX, n.clientY += e[i].clientY; n.clientX = n.clientX / t, n.clientY = n.clientY / t } return n }(e.originalEvent.touches)[n] || 0) - u.offset().left } function k(e, n, t, i) { var o, a, r = e.data("scale") || 1; 1 === n ? e.css({ transform: "translateY(-50%)", "transform-origin": "", top: "50%", left: 0, transition: ".3s all linear" }).data("scale", 1) : (!0 === i && (n = ((n *= r) < .5 ? .5 : 3 < n && 3) || n), i = f(window).height() - e.height() * n, r = u.width() - e.width() * n, o = C(t, "x"), a = C(t, "y"), p.html("x:" + o + " y:" + a), e.css({ top: i < 0 ? Math.max(Math.min(0, -(a - (t.pos && t.pos.top || 0)) * (n - 1)), i) : i / 2, left: r < 0 ? Math.max(Math.min(0, -(o - (t.pos && t.pos.left || 0)) * (n - 1)), r) : r / 2, "transform-origin": "0 0", transform: "scale(" + n + ")", transition: "" }).data("scale", n)) } function M(t, i, o) { var a = null; return function () { var e = this, n = arguments; o && a && (clearTimeout(a) || (a = null)), a = a || setTimeout(function () { t.apply(e, n), a = null }, i) } } function h(e) { e ? (n.bodyOverflow = f("body").css("overflow"), f("body").css("overflow", "hidden")) : f("body").css("overflow", n.bodyOverflow) } f.fn.extend({ yzhanImageViewer: function (d) { var d = { selector: d.selector || "img", attrSelector: d.attrSelector || "src", parentSelector: d.parentSelector, className: d.className, controls: f.extend({ reverseDrag: { x: !1, y: !1 }, canChange: !0 }, d.controls), onChange: d.onChange, onOpen: d.onOpen, onClose: d.onClose, debug: d.debug || !1 }, a = null; function i(e) { switch (e.keyCode) { case 37: l("prev"); break; case 39: l("next"); break; case 82: k(m.children(".current").children("img"), 1); break; case 27: o.call(this) } } function o() { clearTimeout(a), a = setTimeout(function () { f.fn.yzhanImageViewer.close({ $target: y.eq(m.children(".current").index()), onClose: d.onClose }), f(window).off("keydown", i) }, 199) } function r(e) { var n = m.children(".current").children("img"), t = e.originalEvent.touches, i = { x: 0, y: 0 }, o = { $img: n, imgMinLeft: u.width() - n.width() * (n.data("scale") || 1), imgMinTop: f(window).height() - n.height() * (n.data("scale") || 1), x: C(e, "x"), y: C(e, "y") }; t && 2 === t.length && ((o = f.extend(o, { distance: Math.sqrt(Math.pow(t[1].clientX - t[0].clientX, 2) + Math.pow(t[1].clientY - t[0].clientY, 2)), e: e })).e.pos = o.$img.position()), u.on("mousemove touchmove", M(function (e) { i = function (e, n) { var t = n.$img, i = e.originalEvent.touches, o = 0, a = 0; { var r, c, s, l; i && 2 === i.length ? (i = Math.sqrt(Math.pow(i[1].clientX - i[0].clientX, 2) + Math.pow(i[1].clientY - i[0].clientY, 2)), n.distance && (o = .1) && k(t, i / n.distance, n.e, !0)) : (i = t.position().left, r = t.position().top, c = C(e, "x"), e = C(e, "y"), s = t.data("scale") || 1, l = !0, o = d.controls.reverseDrag.x ? n.x - c : c - n.x, a = d.controls.reverseDrag.x ? n.y - e : e - n.y, (o || a) && (1 < s ? (a < 0 && r < 0 ? t.css("top", Math.min(r - a * s, 0) + "px") : 0 < a && r > n.imgMinTop && t.css("top", Math.max(r - a * s, n.imgMinTop) + "px"), o < 0 && i < 0 ? t.css("left", Math.min(i - o * s, 0) + "px") : 0 < o && i > n.imgMinLeft + 1 ? t.css("left", Math.max(i - o * s, n.imgMinLeft) + "px") : l = !1) : l = !1, n.x = c, n.y = e, !1 === l && m.css("marginLeft", parseFloat(m.css("marginLeft")) - o + "px"))) } return { x: o, y: a } }(e, o) }, 16)), f(window).on("mouseup touchend", function () { var e = i, n = (t = o).$img, t = Math.round(-parseFloat(m.css("marginLeft")) / m.width() + ((e.x < 0 ? -.35 : 0 < e.x && .25) || 0)); 0 === e.x && 0 === e.y || setTimeout(function () { clearTimeout(a), n.data("scale") <= 1 && k(n, 1) }, 36), f.fn.yzhanImageViewer.change({ index: t, onChange: d.onChange }), u.off("mousemove touchmove"), f(window).off("mouseup touchend") }) } function c(e) { var n = f(this).children("img"); k(n, 2 === n.data("scale") ? 1 : 2, e), clearTimeout(a) } function s(e, t) { var n = [g], e = e.clientX, i = u.offset().left, o = u.width() / 3, a = m.children().length, r = m.children(".current").index(), c = "ontouchstart" in window; c || ((t || e < i + o) && 0 !== r && n.push(w), (t || i + 2 * o < e) && r < a - 1 && n.push(v)), f.each(n, function (e, n) { !t && "none" !== n.css("display") || n.stop(!0, !0).fadeIn().delay(3500).fadeOut().off("mouseenter touchend").on("mouseenter touchend", function () { f(this).stop(!0, !0).show(), n.timer && clearTimeout(n.timer), c && (n.timer = setTimeout(function () { n.fadeOut() }, 3500)) }).off("mouseleave").on("mouseleave", function () { f(this).stop(!0, !0).delay(200).fadeOut() }) }) } function l(e) { var n = m.children(".current").index(); return "prev" === e ? n-- : n++, f.fn.yzhanImageViewer.change({ index: n, onChange: d.onChange }), !1 } function h() { var e = f(this).index(); return f.fn.yzhanImageViewer.change({ index: e, onChange: d.onChange }), !1 } x = d.selector, this.off("click").on("click", x, function (e) { var n = f(this), t = f(e.delegateTarget); return d.parentSelector && (t = n.parents(d.parentSelector).eq(0)), u.attr("class", "yz-img-viewer" + (d.className && " " + d.className || "")), d.debug ? p.show() : p.hide(), f.fn.yzhanImageViewer.open({ $current: n, $all: y = d.controls.canChange ? t.find(x) : n, onOpen: d.onOpen, attrSelector: d.attrSelector }), u.off("click").on("click", o), u.off("mousedown touchstart mousemove").on("mousedown touchstart", r), m.off("dblclick mousemove touchend").on("mousemove touchend", M(s, 99, !0)).on("dblclick", "div", c), s(e, !0), w.off("click").on("click", function () { return l("prev") }), v.off("click").on("click", function () { return l("next") }), g.off("click").on("click", "div", h), f(window).on("keydown", i), !1 }) } }), f.fn.yzhanImageViewer.open = function (e) { var t, n, i, o = e.$current, a = e.$all || o, r = e.onOpen, c = e.attrSelector, s = 0, e = f(window.event.target), l = a.length; h(!0), m.empty(), g.empty(), a.each(function (e, n) { t = o.get(0) === n ? (s = e, "current") : "", f("<div>").append(f("<img>").attr("src", f(n).attr(c)).mousedown(function (e) { e.preventDefault() })).appendTo(m).addClass(t), 1 < l && f("<div>").appendTo(g).addClass(t) }), m.css("marginLeft", 100 * -s + "%"), 1 === e.length && (a = f(window).scrollTop(), a = e.offset().top - a, n = e.offset().left, i = parseInt(u.css("maxWidth")) || f(window).width(), u.show().css({ top: a, left: n, width: e.width(), height: e.height(), opacity: .5 }).animate({ top: 0, left: (f(window).width() - i) / 2, width: i, height: f(window).height(), opacity: 1 }, 500, function () { f(this).css({ right: 0, left: 0, margin: "auto", width: "100%", height: "100%" }) }), d.show(), r && r(m.children(".current").index(), f.makeArray(m.find(x)), f.makeArray(y))) }, f.fn.yzhanImageViewer.close = function (e) { var n, t, i = e.$target || [], e = e.onClose; 1 <= i.length ? (n = f(window).scrollTop(), n = i.offset().top - n, t = i.offset().left, u.css({ right: "auto", left: u.offset().left, margin: 0 }).animate({ top: n, left: t, width: i.width(), height: i.height(), opacity: .5 }, 500, function () { u.hide() })) : u.fadeOut(), d.hide(), h(!1), e && e(m.children(".current").index(), f.makeArray(m.find(x)), f.makeArray(y)) }, f.fn.yzhanImageViewer.change = function (e) { var n = e.index, e = e.onChange, t = m.children(".current"), i = t.index(), o = m.children().length, n = Math.max(0, Math.min(n, o - 1)); m.stop(!0, !0).animate({ marginLeft: 100 * -n + "%" }, 300), m.children("div").eq(n).addClass("current").siblings().removeClass("current"), g.children("div").eq(n).addClass("current").siblings().removeClass("current"), n !== i && (k(t.children("img"), 1), 0 === n && w.hide(), n === o - 1 && v.hide(), e && e(m.children(".current").index(), t.index(), f.makeArray(m.find(x)), f.makeArray(y))) }, f("body").append(d).append(u) }(jQuery);

