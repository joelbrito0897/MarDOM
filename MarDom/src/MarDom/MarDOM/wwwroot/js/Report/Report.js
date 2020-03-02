$(document).ready(() => {
    getWarehouse();
});

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

const reportForWarehouse = () => {
    var warehouseId = $("#warehouse").find(":selected").val();
    location.href = `api/Report/ForWarehouse/${warehouseId}`;
};

const reportForInOut = () => {
    var moveId = $("#movementType").find(":selected").val();
    location.href = `api/Report/ForInOrOut/${moveId}`;
};