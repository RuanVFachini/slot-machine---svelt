<script lang="ts">
    import Dice from "./dice.svelte";
    import type { DiceResult } from "src/models/models";
    import { onMount } from "svelte";
    import { w3cwebsocket as WebSocketConnection } from "websocket";
    
    export let token: string;

    let diceRotate1 = 0;
    let diceRotate2 = 0;
    let diceRotate3 = 0;

    let socket: WebSocketConnection;

    onMount(() => {
        socket = new WebSocketConnection(`wss://localhost:7123/sort?Token=${token}`);

        socket.onmessage = (message) => {
            if (typeof message.data == 'string') {
                let result = JSON.parse(message.data) as DiceResult;
                diceRotate1 = result.dice1Steps;
                diceRotate2 = result.dice2Steps;
                diceRotate3 = result.dice3Steps;
            }
        };
    });

    export const screen = {
        randomAll(): void {
            let utf8Encode = new TextEncoder();
            let request = utf8Encode.encode(JSON.stringify({sides: 8}));
            socket.send(request);
        }
    }
</script>

<div class="screen">
    <div class="screen-background">
        <div class="screen-image">
            <Dice degre={diceRotate1}></Dice>
        </div>
        <div class="screen-image">
            <Dice degre={diceRotate2}></Dice>
        </div>
        <div class="screen-image">
            <Dice degre={diceRotate3}></Dice>
        </div>
    </div>
</div>

<style scoped>
    .screen-image {
        flex: 1;
        display: flex;
        justify-content: center;
        align-items: center;
        height: 100%;
    }

    .screen {
        /* background-color: blueviolet; */
        /* margin: 30px; */
        flex: 1;
        display: flex;
        padding: 20px;
    }

    .screen-background {
        background-color: rgb(202, 243, 53);
        display: flex;
        flex: 1;
    }
</style>