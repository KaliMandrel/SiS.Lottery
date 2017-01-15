$(document).ready(function () {
    
    $(".submit-draw").on("click", submitDraw);
    $(".submit-results").on("click", submitResults);

    $(".lottery-form").submit(function (e) {
        return false;
    });
    $(".result-form").submit(function (e) {
        return false;
    });

    $("#result-name").on("change", selectChanged);

    populateNames();

});

var submitDraw = function () {

    dataToSend = {
        Name: $(".lottery-form #draw-name").val(),
        Description: $(".lottery-form #draw-description").val(),
        DrawDate: $(".lottery-form #draw-date").val(),
        PrimaryNumberCount: $(".lottery-form #draw-primary-count").val(),
        PrimaryNumberLower: $(".lottery-form #draw-primary-range-min").val(),
        PrimaryNumberUpper: $(".lottery-form #draw-primary-range-max").val(),
        SecondaryNumberCount: $(".lottery-form #draw-secondary-count").val(),
        SecondaryNumberLower: $(".lottery-form #draw-secondary-range-min").val(),
        SecondaryNumberUpper: $(".lottery-form #draw-secondary-range-max").val(),
    }

    $.post("/api/lottery/createdraw", dataToSend)
    .done(function (data) {
        populateNames();
  });
}

var populateNames = function(){
    var options = $("#result-name");
    options.empty();

    $.get("/api/lottery/retrievedraws", function (data) {
        options.append("<option></option>");

        $.each(data, function() {
            options.append($(
                "<option data-primary-min='" + this.PrimaryNumberLower + "'"
                +"data-primary-max='" + this.PrimaryNumberUpper + "'"
                + "data-primary-count='" + this.PrimaryNumberCount + "'"
                + "data-secondary-min='" + this.SecondaryNumberLower + "'"
                + "data-secondary-max='" + this.SecondaryNumberUpper + "'"
                + "data-secondary-count='" + this.SecondaryNumberCount + "'"
                + "data-date='" + this.DrawDate + "'"
                +">" + this.Name + "</option>").val(this.Name));
        });
    });
}

var selectChanged = function(){
    var draw = $("#result-name option:selected")
    var date = new Date(draw.attr("data-date"));
    $("#result-date").val(date.getFullYear() + "-" + ("0" + (date.getMonth() + 1)).slice(-2) + "-" + ("0" + date.getDate()).slice(-2));
    
    var primaryMin = parseInt(draw.attr("data-primary-min"));
    var primaryMax = parseInt(draw.attr("data-primary-max"));
    var primaryCount = parseInt(draw.attr("data-primary-count"));
    $(".result-primary-numbers").empty();
    for (i = 0; i < primaryCount; i++) {
        var select = $("<select class='form-control'></select>");
        for (j = primaryMin; j <= primaryMax; j++) {
            select.append($(
                "<option>" + j + "</option>").val(j));
        }
        
        $(".result-primary-numbers").append(select.attr("id", "result-primary-numbers-" + i));
    }

    var secondaryMin = parseInt(draw.attr("data-secondary-min"));
    var secondaryMax = parseInt(draw.attr("data-secondary-max"));
    var secondaryCount = parseInt(draw.attr("data-secondary-count"));
    $(".result-secondary-numbers").empty();

    for (i = 0; i < secondaryCount; i++) {
        var select = $("<select class='form-control'></select>");
        for (j = secondaryMin; j <= secondaryMax; j++) {
            select.append($(
                "<option>" + j + "</option>").val(j));
        }
        $(".result-secondary-numbers").append(select.attr("id", "result-secondary-numbers-" + i));
    }
    var options = $("#result-name");
}

var submitResults = function () {
    var draw = $("#result-name option:selected");

    var primaryCount = parseInt(draw.attr("data-primary-count"));
    var primaryNumbers = [];

    for (i = 0; i < primaryCount; i++) {
        primaryNumbers.push(parseInt($("#result-primary-numbers-" + i + " option:selected").val()));
    }

    var secondaryCount = parseInt(draw.attr("data-secondary-count"));
    var secondaryNumbers = [];

    for (i = 0; i < secondaryCount; i++) {
        secondaryNumbers.push(parseInt($("#result-secondary-numbers-" + i + " option:selected").val()));
    }

    dataToSend = {
        DrawName: $("#result-name option:selected").val(),
        PrimaryNumbers: primaryNumbers,
        SecondaryNumbers: secondaryNumbers
    }
    $.ajax({
        url: "/api/lottery/updatedraw",
        type: "PUT",
        data: dataToSend,
        success: function (data) {
        }
    });

}