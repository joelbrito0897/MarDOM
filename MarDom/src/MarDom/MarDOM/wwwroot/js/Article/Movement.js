$(document).ready(() => {
    getWarehouse();
    getArticles();
    $("#warehouse").on("change", () => {
     
        var value = $("#warehouse").find(":selected").val();
        if (!isEmpty(value)) {
            valideteAvailability(value);
        }
    });
});

const isEmpty = (value) => {
    return !value || /^\s*$/.test(value);
};

const getArticles = () => {
    $.ajax({
        url: "../api/Article/GetAll",
        method: "GET",
        success: result => {
            var select = $("#articles");
            result.forEach((val) => {
                var element = document.createElement("option");
                element.textContent = val.name;
                element.value = val.id;
                select.append(element);
            });
        }
    });
};

const getWarehouse = () => {
    $.ajax({
        url: "../api/Warehouse/GetAll",
        method: "GET",
        success: result => {
            var select = $("#warehouse");
            result.forEach((val) => {
                var element = document.createElement("option");
                element.textContent = val.name;
                element.value = val.id;
                select.append(element);
              
            });
        },
        error: () => {

        }
    });
};

const valideteAvailability = id => {
    $.ajax({
        url: "../api/Warehouse/QuantityAvailable",
        method: "POST",
        data: { Id: id },
        success: result => {
            if (result > 0) {
                Swal.fire({
                    title: "Disponibilidad",
                    type: "info",
                    text: `Tiene disponibilad para almacenal ${result} articulos.`
                });
                $("#btnAdd").removeAttr('disabled');
            } else {
                Swal.fire({
                    title: "Disponibilidad",
                    type: "info",
                    text: `No hay disponibilidad en este almacen.`
                });
                $("#btnAdd").attr('disabled', 'disabled');
            }            
        },
        error: () => {
            Swal.fire({
                title: "Error",
                type: "info",
                text: "Error al consultar la disponibilidad."
            });
        }
    });
};

const addMovement = () => {
    var obj = new Object();
    obj.MovementType = $("#movementType").find(":selected").val();
    obj.WarehouseId = $("#warehouse").find(":selected").val();
    obj.ArticleId = $("#articles").find(":selected").val();
    obj.Quantity = $("#quantity").val();

    $.ajax({
        url: "../api/Warehouse/AddMovement",
        method:"POST",    
        data: obj,
        success: (result) => {
            if (result === true) {
                Swal.fire(
                    'Guardado!',
                    'Su Movimiento de articulo ha sido realizado.',
                    'success'
                ).then(() => window.location.reload());
            }
        },
        error: () => {
            Swal.fire({
                title: "Error",
                type: "error",
                text: "Error al realizar su movimiento."
            });
        }
    });
};

const isAllowToInsert = () => {
    var quantity = $("#quantity").val();
    var warehouseId = $("#warehouse").find(":selected").val();
    $.ajax({
        url: `../api/Warehouse/IsAllowToInsert/${quantity}/${warehouseId}`,
        method: "Get",
        success: (result) => {
            if (result) {
                addMovement();
            } else {
                Swal.fire({
                    title: "Error",
                    type: "info",
                    text: "No puede inserta una cantidad mayor a la registrada."
                });
            }
        }
    });
}