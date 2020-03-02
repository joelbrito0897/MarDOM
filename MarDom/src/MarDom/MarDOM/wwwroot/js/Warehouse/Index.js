const createWarehouse = () => {
    Swal.fire({
        title: "Crear Almacen",
        type: "info",
        html: `
	<div class="bg-contact2"">
		<div class="container-contact2">
			<div class="wrap-contact2">
				<form class="contact2-form validate-form">


					<div class="wrap-input2 validate-input" data-validate="El nombre es requerido.">
						<input class="input2" type="text" name="name" id="name">
						<span class="focus-input2" data-placeholder="NOMBRE"></span>
					</div>

					<div class="wrap-input2 validate-input" data-validate = "La direccion es requerido.">
						<input class="input2" type="text" name="address" id="address">
						<span class="focus-input2" data-placeholder="DIRECCION"></span>
					</div>

                    <div class="wrap-input2 validate-input" data-validate = "La capacidad de articulos es requerida.">
						<input class="input2" type="number" name="quantity" max="10000" min="0" id="quantity">
						<span class="focus-input2" data-placeholder="CAPACIDAD DE ARTICULOS"></span>
					</div>

					<div class="wrap-input2 validate-input" data-validate = "La descripcion es requerida.">
						<textarea class="input2" name="description" id="description"></textarea>
						<span class="focus-input2" data-placeholder="DESCRIPCION"></span>
					</div>

				</form>
			</div>
		</div>
	</div>`,
        showCancelButton: true,
        confirmButtonText: "Guardar",
        cancelButtonText: "Cancelar",
        confirmButtonColor: "#1abc9c",
        cancelButtonColor: '#d33'
    }).then(result => {
        if (result.value) {
            if (!isFieldBlank("name") && !isFieldBlank("address") && !isFieldBlank("quantity") && !isFieldBlank("description")) {

                var obj = new Object();
                obj.Name = $("#name").val();
                obj.Address = $("#address").val();
                obj.CapacityOfArticlesTotal = $("#quantity").val();
                obj.Description = $("#description").val();

                $.ajax({
                    url: "api/Warehouse/Add",
                    method: "POST",
                    data: obj,
                    success: (result) => {
                        if (result === true) {
                            Swal.fire(
                                'Guardado!',
                                'Su almacen ha sido creado.',
                                'success'
                            ).then(() => window.location.reload());
                        }
                    },
                    error: () => {
                        swal.fire({
                            title: "Error",
                            type: "error",
                            text: "Ha ocurrido un error creando el almacen"
                        }).then(() => window.location.reload());
                    }
                });
            } else {
                swal.fire({
                    title: "Complete los campos",
                    type: "warning",
                    text: "No puede dejar campos en blanco."
                });
            }

           
        }
    });
};

const updateWarehouse = (id, name, address, quantity, description) => {
    Swal.fire({
        title: "Acrualizar Almacen",
        type: "info",
        html:`<div class="bg-contact2"">
		<div class="container-contact2">
			<div class="wrap-contact2">
				<form class="contact2-form validate-form">


					<div class="wrap-input2 validate-input" data-validate="El nombre es requerido.">
						<input class="input2" type="text" name="name" id="name" value="${name}">
						<span class="focus-input2" data-placeholder="NOMBRE"></span>
					</div>

					<div class="wrap-input2 validate-input" data-validate = "La direccion es requerido.">
						<input class="input2" type="text" name="address" id="address" value="${address}">
						<span class="focus-input2" data-placeholder="DIRECCION"></span>
					</div>

                    <div class="wrap-input2 validate-input" data-validate = "La capacidad de articulos es requerida.">
						<input class="input2" type="number" name="quantity" max="10000" min="0" id="quantity" value="${quantity}">
						<span class="focus-input2" data-placeholder="CAPACIDAD DE ARTICULOS"></span>
					</div>

					<div class="wrap-input2 validate-input" data-validate = "La descripcion es requerida.">
						<textarea class="input2" name="description" id="description">${description}</textarea>
						<span class="focus-input2" data-placeholder="DESCRIPCION"></span>
					</div>

				</form>
			</div>
		</div>
	</div>`,
        showCancelButton: true,
        confirmButtonText: "Actualizar",
        cancelButtonText: "Cancelar",
        confirmButtonColor: "#1abc9c",
        cancelButtonColor: '#d33'
    }).then(result => {
        if (result.value) {
            if (!isFieldBlank("name") && !isFieldBlank("address") && !isFieldBlank("quantity") && !isFieldBlank("description")) {

                var obj = new Object();
                obj.Id = id;
                obj.Name = $("#name").val();
                obj.Address = $("#address").val();
                obj.CapacityOfArticlesTotal = $("#quantity").val();
                obj.Description = $("#description").val();

                $.ajax({
                    url: "api/Warehouse/Update",
                    method: "POST",
                    data: obj,
                    success: (result) => {
                        if (result === true) {
                            Swal.fire(
                                'Actualizado!',
                                'Su almacen ha sido actualizado.',
                                'success'
                            ).then(() => window.location.reload());
                        }
                    },
                    error: () => {
                        swal.fire({
                            title: "Error",
                            type: "error",
                            text: "Ha ocurrido un error actualizando el almacen"
                        }).then(() => window.location.reload());
                    }
                });
            } else {
                swal.fire({
                    title: "Complete los campos",
                    type: "warning",
                    text: "No puede dejar la campos en blanco."

                });
            }


        }
    });
};

const deleteWarehouse = (id) =>
{
    Swal.fire({
        title: "Borrar Almacen",
        type: "info",
        text: "Esta seguro de borrar?",
        showCancelButton: true,
        confirmButtonText: "Borrar",
        cancelButtonText: "Cancelar",
        confirmButtonColor: "#1abc9c",
        cancelButtonColor: '#d33'
    }).then(result => {
        if (result.value) {
            $.ajax({
                url: "api/Warehouse/Disable",
                method: "POST",
                data: { Id: id },
                success: (result) => {
                    if (result === true) {
                        Swal.fire(
                            'Borrado!',
                            'Su almacen ha sido Borrado.',
                            'success'
                        ).then(() => window.location.reload());
                    }
                },
                error: () => {
                    swal.fire({
                        title: "Error",
                        type: "error",
                        text: "Ha ocurrido un error borrando el almacen."

                    }).then(() => window.location.reload());
                }
            });
        }
    });
};