const uniq_dataTable_show = '<label class="input-group mb-3"><div class="input-group-prepend"><span class="input-group-text" title="Show"><i class="fas fa-eye"> </i></span></div>';
const uniq_dataTable_search = '<div class="input-group-prepend"><span class="input-group-text" title="Search"><i class="fas fa-search"> </i></span></div>';

function getToday() {
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1; // January is 0
    var yyyy = today.getFullYear();

    if (dd < 10) {
        dd = '0' + dd

    } // end if day less than 10

    if (mm < 10) {
        mm = '0' + mm

    } // end if month less than 10

    today = yyyy + '-' + mm + '-' + dd;

    return today;

} // end function getToday

function getYesterday() {
    var today = new Date(new Date().setDate(new Date().getDate() - 1));

    var dd = today.getDate();
    var mm = today.getMonth() + 1; //January is 0
    var yyyy = today.getFullYear();

    if (dd < 10) {
        dd = '0' + dd

    } // end if day less than 10

    if (mm < 10) {
        mm = '0' + mm

    } // end if month less than 10

    today = yyyy + '-' + mm + '-' + dd;

    return today;

} // end function getYesterday

function clearMsgbox(id) {
    $(id).removeClass('alert-success');
    $(id).removeClass('alert-info');
    $(id).removeClass('alert-warning');
    $(id).removeClass('alert-danger');
    $(id).html('&nbsp;');

} // end function clearMsgbox

function complareDate(stD, enD) {
    var startDate = new Date(stD);
    var endDate = new Date(enD);

    var dd = 0;
    var mm = 0;
    var yyyy = 0;

    if (endDate < startDate) {
        dd = startDate.getDate();
        mm = startDate.getMonth() + 1; //January is 0
        yyyy = startDate.getFullYear();

        //alert('Can\'t select end date before start date!');

    } else {
        dd = endDate.getDate();
        mm = endDate.getMonth() + 1; //January is 0
        yyyy = endDate.getFullYear();

    } // end if complare

    if (dd < 10) {
        dd = '0' + dd

    } // end if day less than 10

    if (mm < 10) {
        mm = '0' + mm

    } // end if month less than 10

    return yyyy + '-' + mm + '-' + dd;

} // end function complareDate

function highlightActiveMenuItem() {
    var path = window.location.pathname.replace('//', '/');

    $('#navbarColor02 a[href="' + path + '"]').addClass('active');

    if ($('#navbarColor02 a[href="' + path + '"]').hasClass('dropdown-item')) {
        $('#navbarColor02 a[href="' + path + '"]').parents('li').children('a').addClass('active'); // header

    } // end if dropdown-item

} // end function highlightActiveMenuItem

function commaSeparateNumber(val) {
    while (/(\d+)(\d{3})/.test(val.toString())) {
        val = val.toString().replace(/(\d+)(\d{3})/, '$1' + ',' + '$2');
    }

    return val;

} // end function: commaSeparateNumber

function pad(str, max) {
    str = str.toString();
    return str.length < max ? pad('0' + str, max) : str;

} // end function: pad

function keyupDelay(callback, ms) {
    var timer = 0;
    return function () {
        var context = this, args = arguments;
        clearTimeout(timer);
        timer = setTimeout(function () {
            callback.apply(context, args);
        }, ms || 0);
    }; // end return:
}; // end function: keyupDelay

(function ($) {
    $.fn.inputFilter = function (inputFilter) {
        return this.on("input keydown keyup mousedown mouseup select contextmenu drop", function () {
            if (inputFilter(this.value)) {
                this.oldValue = this.value;
                this.oldSelectionStart = this.selectionStart;
                this.oldSelectionEnd = this.selectionEnd;
            } else if (this.hasOwnProperty("oldValue")) {
                this.value = this.oldValue;
                this.setSelectionRange(this.oldSelectionStart, this.oldSelectionEnd);
            } else {
                this.value = "";
            }
        });
    };
}(jQuery)); // end function inputFilter

function regInputFilter(value, regex, limit) {
    let rex = '';

    switch (regex) {
        case 'int':
            rex = /^-?\d*$/;
            break;
        case 'uint':
            rex = /^\d*$/;
            break;
        case 'intLimit':
            rex = /^\d*$/;
            break;
        case 'float':
            rex = /^-?\d*[.,]?\d*$/;
            break;
        case 'currency':
            rex = /^-?\d*[.,]?\d{0,2}$/;
            break;
        case 'latin':
            rex = /^[a-z]*$/i;
            break;
        case 'hex':
            rex = /^[0-9a-f]*$/i;
            break;
    } // end switch: regex

    if (regex == 'intLimit') {
        return rex.test(value) && (value === "" || parseInt(value) <= limit);
    } else {
        return rex.test(value);
    } // end if: rex is intLimit
} // end function: regInputFilter

function showMessage(header, body) {
    $('#divMsgBox').removeClass('hidden');
    $('#divMsgBox').removeClass(function (key, className) {
        return (className.match(/\balert-\S+/g) || []).join(' ');
    });
    $('#divMsgBox').addClass('alert-' + header);
    $('#divMsgBox').html('<i class="fas fa-exclamation-triangle"></i> ' + body);

    $('html, body').stop().animate({ scrollTop: 0 }, 500, 'swing');
} // end function: showMessage

function hideMessage() {
    $('#divMsgBox').addClass('hidden');
} // end function: hideMessage

function getDataToSelect(url, sel, val, txt, title, arr) {
    if (url != '') {
        $.ajax({
            url: url,
            type: 'POST',
            success: function (data) {
                if (data.Result) {
                    for (let key in data.List) {
                        const id = data.List[key].id;
                        const name = data.List[key].name;

                        sel.append(`<option value="${val == 1 ? name : id}" title="${title == 1 ? name : id}">${txt == 1 ? name : id}</option>`);
                    } // end for: data.List
                } else {
                    console.log(data.Message);
                } // end if: data.Result is true
            }, // end success: data
            error: function (err) {
                console.log(err);
            } // end error: err
        }); // end ajax:
    } else {
        const item = arr;
        const items = item.split(',');

        for (let key in items) {
            sel.append(`<option value="${items[key]}">${items[key]}</option>`);
        } // end for: items
    } // end if: arr is null
} // end function: getDataToSelect

function getLocationHref() {
    const url = window.location.href;
    const arr = url.split('/');
    let path;

    if (arr[2].substring(0, 9) == 'localhost') {
        path = arr[0] + '//' + arr[2];
    } else {
        path = arr[0] + '//' + arr[2] + '/' + arr[3] + '/' + arr[4];
    } // end if: check on localhost or not

    return path;
} // end function: getLocationHref