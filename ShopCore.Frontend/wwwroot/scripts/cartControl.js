function AddToCart(pID) {
    if (!CheckItem("activeUser"))
        window.location.replace("login.html");
    if (localStorage.getItem("cart" + GetItem("activeUser")===null))
        GetCart();

    var cart = JSON.parse(localStorage.getItem("cart" + GetItem("activeUser")));

    var request = {
        cart: cart,
        pID: pID
    };

    $.ajax({
        type: "POST",
        accepts: "application/json",
        url: "https://localhost:44344/api/order",
        contentType: "application/json",
        data: JSON.stringify(request),
        success: function (result) {
            localStorage.setItem("cart" + GetItem("activeUser"), result);
            alert("Item added to the cart");
        },
        error: function (xhr, textStatus, errorThrown) {
            var err = JSON.parse(xhr.responseText).ERROR.errors[0].errorMessage;
            alert(err);
        }
    });
}

function GetCart() {
    $.ajax({
        type: "GET",
        accepts: "application/json",
        url: "https://localhost:44344/api/order",
        contentType: "application/json",
        success: function (result) {
            //alert(result); //provjeri aktivnog usera na login i na pocetno otvaranje
            localStorage.setItem("cart" + GetItem("activeUser"), result);
        },
        error: function (xhr, textStatus, errorThrown) {
            var err = JSON.parse(xhr.responseText).ERROR.errors[0].errorMessage;
            alert(err);
        }
    });
}

function RemoveFromCart(pID) {
    var cart = JSON.parse(localStorage.getItem("cart" + GetItem("activeUser")));

    var request = {
        cart: cart,
        pID: pID
    };

    $.ajax({
        type: "DELETE",
        accepts: "application/json",
        url: "https://localhost:44344/api/order/",
        contentType: "application/json",
        data: JSON.stringify(request),
        success: function (result) {
            localStorage.setItem("cart" + GetItem("activeUser"), result);
            window.location.reload();
        },
        error: function (xhr, textStatus, errorThrown) {
            var err = JSON.parse(xhr.responseText).ERROR.errors[0].errorMessage;
            alert(err);
        }
    });
}

function Order() {
    var cart = JSON.parse(localStorage.getItem("cart" + GetItem("activeUser")));

    var order = {
        cart: cart,
        uID: GetItem("activeUser")
    };

    $.ajax({
        type: "POST",
        accepts: "application/json",
        url: "https://localhost:44344/api/order/complete",
        contentType: "application/json",
        data: JSON.stringify(order),
        success: function (result) {
            ClearLocal();
            window.location.replace("orders.html");
        },
        error: function (xhr, textStatus, errorThrown) {
            var err = JSON.parse(xhr.responseText).ERROR.errors[0].errorMessage;
            alert(err);
        }
    });
}

function ClearLocal() {
    localStorage.clear();
}

function ClearMissingCarts(id) {
    var x = localStorage.getItem("cart" + id);
    if (x != null)
        localStorage.removeItem("cart" + id);
}