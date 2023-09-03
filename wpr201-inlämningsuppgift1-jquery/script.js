console.log('Script file found');


// Samla alla jQuery funktioner så de körs när sidan är laddad
$(document).ready(function() {
console.log('jQuery is activated');

  toggleElementOnClick("#toggle-element");
  changeStyleOnHover("#self-description");
  performInitialAnimation("#contentbox-middle");
  changeTextOnDoubleClick("#trigger-element", "#text-element");
  toggleDarkMode();  
});

// 1. Toggle visibility of an element when it's clicked
function toggleElementOnClick(selector) {
  $(selector).click(function() {
    $(this).toggle();
  });
}

// 2. Change the style of an element when the mouse hovers over it
function changeStyleOnHover(selector) {
  $(selector).hover(function() {
    $(this).toggleClass("new-style");
  });
}

// 3. Perform some animation on an element
function performInitialAnimation(selector) {
  console.log('Animation triggered');
  
  $(selector).animate({left: '100px', top: '100px'}, 1000)
             .animate({left: '0px', top: '0px'}, 1000);
}

// 4. Change the text of an element when another element is double-clicked
function changeTextOnDoubleClick(triggerSelector, targetSelector) {
  $(triggerSelector).dblclick(function() {
    $(targetSelector).text("New Text");
  });
}

// 5. Toggla "Dark Mode"
function toggleDarkMode() {
  $('#dark-mode-toggle').click(function() {
    $('body').toggleClass('dark-mode');
  });
}
