// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
console.log("test latihan");

let array = [1, 2, 3, "test"];
console.log(array);


array.push("halo");
console.log(array);

array.pop();
console.log(array);

array.unshift("depan");
console.log(array);

array.shift();
console.log(array);

console.log("array MULTI")
let arrayMulti = ['a', 'b', 'c', [1, 2, 3], true];
console.log(arrayMulti[3][1]);

//deklarasi object
let mhs = {
    nama: "rizki",
    nim: "170914060",
    jurusan: "TI",
    umur: 23,
    hobi: ["mancing", "ngegame", "olahraga"],
    isActive: true
}

console.log(mhs);

console.log(mhs.hobi[2]);

const hobis = [];
for (var i = 0; i < mhs.hobi.length; i++) {
    hobis.push(mhs.hobi[i]);
}

console.log(hobis);


const user = {};

user.username = "rizki";
user.nrp = "170914060";

console.log(user.nrp);

user.username = "fajrin";
console.log(user);

key = "password";
console.log(user[key]);

const csv = "1|2|3";

const [one, two, three] = csv.split("|");
console.log(three);



//jika species cat masukan ke dalam variable onlycat
//jika species fish masukan ke dalam ganti classnya jadi non mamalia

//let onlyCat = [];

//console.log(onlyCat)

//animals.forEach(function (animals) {
//    if (animals.species == "cat") {
//        onlyCat.push(animals)
//    } else {
//        animals.class.name = "non-invertebrata";
//    }
//})

//onlyCat.forEach(function (cat) {
//    console.log(cat);
//})
const animals = [
    { name: "Garfield", species: "cat", class: { name: "mamalia" } },
    { name: "Nemo", species: "fish", class: { name: "invertebrata" } },
    { name: "Tom", species: "cat", class: { name: "mamalia" } },
    { name: "Bruno", species: "fish", class: { name: "invertebrata" } },
    { name: "Carlo", species: "cat", class: { name: "mamalia" } },
]
console.log(animals);

const OnlyCat = [];

for (var i = 0; i < animals.length; i++) {
    if (animals[i].species == "fish") {
        animals[i].class.name = "Non-Mamalia";
    }
    else if (animals[i].species == "cat") {
        OnlyCat.push(animals[i]);
    }
}

console.log("Animals");
console.log(animals);

console.log("Cats");
console.log(OnlyCat);











