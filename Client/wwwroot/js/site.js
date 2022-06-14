// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
//const currentLocation = location.href;
//const menuItem = document.querySelectorAll('a');
//const menuLength = menuItem.length;
//for (let i = 0; i < menuLength; i++) {
//    if (menuItem[i].href === currentLocation) {
//        menuItem[i].className = "active";
//    } else {
//        menuItem[i].className = "";
//    }
//}

//const myElement = document.getElementById("card1");
//myElement.className = "card text-white bg-danger mb-3";

//document.getElementById("card1").addEventListener("click", ubahWarna1);

function ubahWarna1() {
    const getClass = document.getElementById("card1");
    if (getClass.className == "card text-white bg-info mb-3") {
        document.getElementById("card1").className = "card text-white bg-warning mb-3";
    } else {
        document.getElementById("card1").className = "card text-white bg-info mb-3";
    }
}

//document.getElementById("btn2").addEventListener("paragraf", ubahParagraf);

function ubahParagraf() {
    const getPar = document.getElementById("paragraf");
    const getBtn = document.getElementById("btn2");
    if (getPar.className == "card-text text-justify") {
        document.getElementById("paragraf").className = "card-text text-left";
        getBtn.innerHTML = "Change to Rigth";

    } else if (getPar.className == "card-text text-left") {
        document.getElementById("paragraf").className = "card-text text-right";
        getBtn.innerHTML = "Change to Center";
    } else if (getPar.className == "card-text text-right") {
        document.getElementById("paragraf").className = "card-text text-center";
        getBtn.innerHTML = "Change to Justify";
    } else {
        document.getElementById("paragraf").className = "card-text text-justify";
        getBtn.innerHTML = "Change to Left";
    }
}

function ubahWarna() {
    const getClass = document.getElementById("card2");
    const getButton = document.getElementById("btn1");
    if (getButton.value == "red") {
        getButton.value = "yellow"
        getClass.className = "card text-white bg-warning mb-3";
        getButton.className = "btn btn-warning";
        getButton.innerHTML = "Change to Green";
    } else if (getButton.value == "yellow") {
        getButton.value = "green"
        getClass.className = "card text-white bg-success mb-3";
        getButton.className = "btn btn-success";
        getButton.innerHTML = "Change to Blue";
    } else if (getButton.value == "green") {
        getButton.value = "blue"
        getClass.className = "card text-white bg-primary mb-3";
        getButton.className = "btn btn-primary";
        getButton.innerHTML = "Change to Red";
    } else {
        getButton.value = "red"
        getClass.className = "card text-white bg-danger mb-3";
        getButton.className = "btn btn-danger";
        getButton.innerHTML = "Change to Yellow";
    }
}

//JQUERY


$.ajax({
    url: "https://pokeapi.co/api/v2/pokemon/",
}).done((result) => {
    console.log(result.results);
    let text = "";
    $.each(result.results, function (key, val) {
        console.log(val.name);
        text += `<tr>
                    <td>${key+1}</td>
                    <td>${val.name}</td>
                    <td>
                        <button type="button" onclick="detailPoke('https://pokeapi.co/api/v2/pokemon/${val.name}')" class="btn btn-primary" data-toggle="modal" data-target="#modalDetail">
                                Detail
                        </button>
                    </td>
                </tr>`
    });
    //console.log(text);
    $("#tablePoke").html(text);
}).fail((error) => {
    console.log(error);
})

function detailPoke(url) {
    $.ajax({
        url
    }).done(e => {
        console.log(e);
        $("#imgPoke").attr("src", e.sprites.other.dream_world.front_default);
        $("#imgPoke2").attr("src", e.sprites.other.home.front_default);
        $("#imgPoke3").attr("src", e.sprites.other.home.front_shiny);
        $(".namePoke").text(e.name);
        $("#base_experience").width(e.base_experience);
        $("#height").text(e.height);
        $("#weight").text(e.weight);
        $("#order").text(e.order);
        $("#abil").text(e.abilities[0].ability.name);
        $("#hp").width(e.stats[0].base_stat+"%");
        $("#attack").width(e.stats[1].base_stat+"%");
        $("#defense").width(e.stats[2].base_stat+"%");
        $("#special-attack").width(e.stats[3].base_stat+"%");
        $("#special-defense").width(e.stats[4].base_stat+"%");
        $("#speed").width(e.stats[5].base_stat + "%");

        texts = "",
            $.each(e.types, function (val) {
                texts += `<span class="badge badge-pill badge-success">${e.types[val].type.name}</span>`
            });
        console.log(texts);
        $("#types").html(texts);


        
    })
    //console.log(url);
    //$("#imgPoke").html();
}

//$.ajax({
//    url: "https://pokeapi.co/api/v2/pokemon/",
//}).done((res) => {
//    console.log(res.results);
//    let text = "";
//    $.each(res.results, function (key, val) {
//        console.log(val.abilities);
        
//        //text += `<tr>
//        //            <td>${key + 1}</td>
//        //            <td>${val.name}</td>
//        //            <td>
//        //                <button type="button" onclick="detailPoke('https://pokeapi.co/api/v2/pokemon/${val.name}')" class="btn btn-primary" data-toggle="modal" data-target="#modalDetail">
//        //                        Detail
//        //                </button>
//        //            </td>
//        //        </tr>`
//    });
//    //console.log(text);
//    //$("#tablePoke").html(text);
//}).fail((error) => {
//    console.log(error);
//})

