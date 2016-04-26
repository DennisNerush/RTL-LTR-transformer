# RTL-LTR-transformer
Transforms your CSS to SCSS that is ready to support RTL and LTR application.

This tool transforms your current CSS or SCSS file that uses implicit layout directions like margin-left/float:right/padding:10px 5px 30px 2px and many more to a generic SCSS file that uses SASS functions in order to specify the right direction for your style. It will not modify css selectors that don't effect layout direction.

.notification {
    top: 0;
@include right(0);
    width: 100%;
    background-color: #0292d9;
@include text-align(left);
@include margin-right(10px);
    padding:5px 0
}

#SASS mixins for RTL-LTR support
@mixin float($dir) {
  @if $dir == left {
    float: $default-float;
    } @else if $dir == right {
      float: $opposite-float;
    } @else {
      float: $dir;
  }
}

@mixin text-align($dir) {
  @if $dir == left {
    text-align: $default-float;
    } @else if $dir == right {
      text-align: $opposite-float;
    } @else {
      text-align: $dir;
  }
}

@mixin margin($up, $right, $down, $left) {
  @if $default-float == left {
    margin: $up $right $down $left;
  }
  @else{
      margin: $up $left $down $right;
  }
}
@mixin padding($up, $right, $down, $left) {
  @if $default-float == left {
    padding: $up $right $down $left;
  }
  @else{
      padding: $up $left $down $right;
  }
}

@mixin padding-left($unit) {
  padding-#{$default-float}: $unit;
}

@mixin padding-right($unit) {
  padding-#{$opposite-float}: $unit;
}


@mixin margin-left($unit) {
  margin-#{$default-float}: $unit;
}

@mixin margin-right($unit) {
  margin-#{$opposite-float}: $unit;
}

@mixin right($unit) {
 $opposite-float: $unit;
}

@mixin left($unit) {
 $default-float: $unit;
}

Now all that's left is to define the root directions:
// RTL languages directions.
$default-float:         right;
$opposite-float:         left;

$default-direction:       rtl;
$opposite-direction:      ltr;

// LTR languages directions.
$default-float:          left;
$opposite-float:        right;

$default-direction:       ltr;
$opposite-direction:      rtl;

And here is how you use it:

