const createCategory = () => {
    Swal.fire({
        title: "Crear Categoria",
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
            if (!isFieldBlank("name")) {
                var name = $("#name").val();
                $.ajax({
                    url: "api/Category/Add",
                    method: "POST",
                    data: { Name: name },
                    success: (result) => {
                        if (result === true) {
                            Swal.fire(
                                'Guardado!',
                                'Su categoria ha sido creada.',
                                'success'
                            ).then(() => window.location.reload());
                        }
                    },
                    error: () => {
                        swal.fire({
                            title: "Error",
                            type: "error",
                            text: "Ha ocurrido un error creando la categoria",
                            timer: 2000,
                            width: 600,
                            height: 800

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

const updateCategory = (id, name) => {
    Swal.fire({
        title: "Acrualizar Categoria",
        type: "info",
        html: `<div class="bg-contact2"">
		<div class="container-contact2">
			<div class="wrap-contact2">
				<form class="contact2-form validate-form">

					<div class="wrap-input2 validate-input" data-validate="El nombre es requerido.">
						<input class="input2" type="text" name="name" id="name" value="${name}">
						<span class="focus-input2" data-placeholder="NOMBRE"></span>
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
            if (!isFieldBlank("name")) {
                var obj = new Object();
                obj.Id = id;
                obj.Name = $("#name").val();

                $.ajax({
                    url: "api/Category/Update",
                    method: "POST",
                    data: obj,
                    success: (result) => {
                        if (result === true) {
                            Swal.fire(
                                'Actualizado!',
                                'Su categoia ha sido actualizado.',
                                'success'
                            ).then(() => window.location.reload());
                        }
                    },
                    error: () => {
                        swal.fire({
                            title: "Error",
                            type: "error",
                            text: "Ha ocurrido un error actualizando la caegoria"
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

const deleteCategory = (id) => {
    Swal.fire({
        title: "Borrar categoria",
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
                url: "api/Category/Disable",
                method: "POST",
                data: { Id: id },
                success: (result) => {
                    if (result === true) {
                        Swal.fire(
                            'Borrado!',
                            'Su categoria ha sido Borrado.',
                            'success'
                        ).then(() => window.location.reload());
                    }
                },
                error: () => {
                    swal.fire({
                        title: "Error",
                        type: "error",
                        text: "Ha ocurrido un error borrando la categoria."

                    }).then(() => window.location.reload());
                }
            });
        }
    });
};