const getCategory = (id) => {
    $.ajax({
        url: "api/Category/GetAll",
        method: "GET",
        success: result => {
            var select = $("#category");
            result.forEach((val) => {
                var element = document.createElement("option");
                element.textContent = val.name;
                element.value = val.id;
                select.append(element);
                if (id !== "") {
                    $(`#category option[value="${id}"]`).prop('selected', true);
                }
            });
        },
        error: () => {

        }
    });
};

const verifySelect = (id) => {
    var value = $("#" + id).find(":selected").val();
   return value === "" ? false: true;
};

const createArticle = () =>
{
    getCategory();
    Swal.fire({
        title: "Crear Articulo",
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

                    <div class="wrap-input2 validate-input" data-validate="El nombre es requerido.">
                        <select id="category" class="form-control">
                            <option value="">Please select...</option>
                        </select>
					</div>	
                    <div class="wrap-input2 validate-input" data-validate="El precio es requerido.">
						<input class="input2" type="number" name="price" id="price">
						<span class="focus-input2" data-placeholder="Precio"></span>
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
            if (!isFieldBlank("name") && !isFieldBlank("price") && verifySelect("category")) {
                var obj = new Object();
                obj.Name = $("#name").val();
                obj.Price = $("price").val();
                obj.Categoy = $("#category").find(":selected").val();

                $.ajax({
                    url: "apo/Article/Add",
                    method: "POST",
                    data: obj,
                    success: (result) => {
                        if (result === true) {
                            Swal.fire(
                                'Guardado!',
                                'Su articulo ha sido creado.',
                                'success'
                            ).then(() => window.location.reload());
                        }
                    },
                    error: () => {
                        swal.fire({
                            title: "Error",
                            type: "error",
                            text: "Ha ocurrido un error creando del articulo"
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

const updateArticle = (id, name, price, category) => {
    
    getCategory(category);
    Swal.fire({
        title: "Crear Articulo",
        type: "info",
        html: `
	<div class="bg-contact2"">
		<div class="container-contact2">
			<div class="wrap-contact2">
				<form class="contact2-form validate-form">

                    <div class="wrap-input2 validate-input" data-validate="El nombre es requerido.">
						<input class="input2" type="text" name="name" id="name" value="${name}">
						<span class="focus-input2" data-placeholder="NOMBRE"></span>
					</div>	

                    <div class="wrap-input2 validate-input" data-validate="El nombre es requerido.">
                        <select id="category" class="form-control">
                            <option value="">Please select...</option>
                        </select>
					</div>	
                    <div class="wrap-input2 validate-input" data-validate="El precio es requerido.">
						<input class="input2" type="number" name="price" id="price" value="${price}">
						<span class="focus-input2" data-placeholder="Precio"></span>
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
            if (!isFieldBlank("name") && !isFieldBlank("price") && verifySelect("category")) {
                var obj = new Object();
                obj.id = id;
                obj.Name = $("#name").val();
                obj.Price = $("#price").val();
                obj.CategoryId = $("#category").find(":selected").val();
             
                $.ajax({
                    url: "api/Article/Update",
                    method: "POST",
                    data: obj,
                    success: (result) => {
                        if (result === true) {
                            Swal.fire(
                                'Guardado!',
                                'Su articulo ha sido actualizado.',
                                'success'
                            ).then(() => window.location.reload());
                        }
                    },
                    error: () => {
                        swal.fire({
                            title: "Error",
                            type: "error",
                            text: "Ha ocurrido un error actualizando el articulo."
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

const deleteArticle = (id) => {
    Swal.fire({
        title: "Borrar Articulo",
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
                url: "api/Article/Disable",
                method: "POST",
                data: { Id: id },
                success: (result) => {
                    if (result === true) {
                        Swal.fire(
                            'Borrado!',
                            'Su articulo ha sido Borrado.',
                            'success'
                        ).then(() => window.location.reload());
                    }
                },
                error: () => {
                    swal.fire({
                        title: "Error",
                        type: "error",
                        text: "Ha ocurrido un error borrando el articulo."

                    }).then(() => window.location.reload());
                }
            });
        }
    });};