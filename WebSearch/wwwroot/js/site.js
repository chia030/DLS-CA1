// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let ViewModel = function () {

    let me = this;
    
    me.searchTerms = ko.observable();
    me.hits = ko.observable();
    me.results = ko.observableArray();
    me.timeUsed = ko.observable();
    
    me.search = function() {
        console.log("calling search function")
        $.ajax({
            url: "http://localhost:8071/LB/SearchQuery?query=" + me.searchTerms() + "&maxAmount=10",
            success: function(data) {
                data = JSON.parse(data);
                console.log(data);
                me.hits(data.Hits);
                me.timeUsed(data.TimeUsed);
                me.results.removeAll();
                data.DocumentHits.forEach(function(hit) {
                    me.results.push(hit);
                });
                console.log(me.hits());
                console.log(me.timeUsed());
            }
        });
    }

};

ko.applyBindings(new ViewModel());