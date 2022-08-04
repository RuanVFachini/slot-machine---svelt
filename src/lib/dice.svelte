<script lang="ts">
import { cubicInOut } from "svelte/easing";

import { tweened } from "svelte/motion";
    export let degre = 0;

    const rotateDegre = tweened(0, {
        duration: 300,
        easing: cubicInOut
    });

    $: {
        $rotateDegre = degre;
    }

</script>

<div id="D3Cube" style="transform: rotateX({$rotateDegre}deg)">
    <div id="side1"></div>
    <div id="side2"></div>
    <div id="side3"></div>
    <div id="side4"></div>
    <div id="side5"></div>
    <div id="side6"></div>
    <div id="side7"></div>
    <div id="side8"></div>
</div>

<style scoped>
    #D3Cube {
        width: 110px;
        height: 50px;
        transform-style: preserve-3d;
        margin: auto;
        position: relative;
        transform-style: preserve-3d;
    }

    #D3Cube > div {
        position: absolute;
        width: 100%;
        height: 100%;
        float: left;
        overflow: hidden;
    }

    #side1 {
        transform: rotatex(0deg) translateX(0px) translateY(0px) translateZ(60px);
        background-color: rgb(13, 241, 32);
    }

    #side2 {
        transform: rotatex(45deg) translateX(0px) translateY(0px) translateZ(60px);
        background-color: rgb(1, 107, 18);
    }

    #side3 {
        transform: rotatex(90deg) translateX(0px) translateY(0px) translateZ(60px);
        background-color: rgb(23, 77, 253);
    }

    #side4 {
        transform: rotatex(135deg) translateX(0px) translateY(0px) translateZ(60px);
        background-color: rgb(3, 188, 245);
    }

    #side5 {
        transform: rotatex(180deg) translateX(0px) translateY(0px) translateZ(60px);
        background-color: rgb(248, 31, 31);
    }

    #side6 {
        transform: rotatex(225deg) translateX(0px) translateY(0px) translateZ(60px);
        background-color: rgb(253, 94, 21);
    }

    #side7 {
        transform: rotatex(270deg) translateX(0px) translateY(0px) translateZ(60px);
        background-color: rgb(221, 236, 13);
    }

    #side8 {
        transform: rotatex(315deg) translateX(0px) translateY(0px) translateZ(60px);
        background-color: rgb(255, 16, 215);
    }


</style>