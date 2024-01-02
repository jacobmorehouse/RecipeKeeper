// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
	tinymce.init({
		selector: 'textarea.tinymce',  // change this value according to your HTML
		promotion: false,
		height: 300,
		width: 700,
		menubar: 'edit view format',
		statusbar: false
	});
});