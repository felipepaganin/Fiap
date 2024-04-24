
window.blazorHelpers = {
    openModal: function (id) {
        var modal = new bootstrap.Modal(document.getElementById(id));
        modal.show();
    },
    closeModal: function (id) {
        var modalElement = document.getElementById(id);
        var modalInstance = bootstrap.Modal.getInstance(modalElement);
        if (modalInstance) {
            modalInstance.hide();
        }
    }
};
