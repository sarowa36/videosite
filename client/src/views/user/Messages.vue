<script setup>
import Textbox from '../../components/Textbox.vue';
import { Message } from "../../models/Message";
import { HubConnection, HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import router from '../../router';
import { RouterLink } from 'vue-router';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
import {useScreen} from "vue-screen";

const screen=useScreen();
</script>
<template>
    <div class="container content">
        <div class="row justify-content-center">
            <div v-if="screen.width > 768 || screen.width < 768 && !this.$route.params.id" class="friend_list col-md-4">
                <!-- <Textbox type="search" placeholder="Ara" class="mb-3" v-model="searchText"></Textbox> -->
                <RouterLink v-for="i in userList" :to="'/user/messages/' + i.id" class="mb-3">
                    <img :src="i.imageLink" alt="">
                    <div class="friend_detail">
                        <div class="friend_name">
                            {{ i.userName }}
                        </div>
                        <span class="friend_last_message">
                            {{ i.lastMessage?.text }}
                        </span>
                    </div>
                </RouterLink>
            </div>
            <div v-if="this.$route.params.id" class="col-md-8 row">
                <div class="col-12 messaging_person d-flex align-items-center">
                    <button class="mobile_return_inbox_btn" @click="returnMessageList"><FontAwesomeIcon icon="arrow-left"></FontAwesomeIcon></button>
                    <img :src="currentMessagingUser.imageLink" alt="">
                    <span>{{ currentMessagingUser.userName }}</span>
                </div>
                <div class="col-12 message_list" ref="messageList">
                    <div v-for="i in messageList"
                        :class="'col-12 message ' + (i.toId == USER.id ? 'message_got' : 'message_send')" :id="i.id">
                        <img :src="i.toId == USER.id ? images.toImage : images.fromImage" alt="">
                        <span>{{ i.text }}</span>
                    </div>
                </div>
                <div class="col-12 write_message_section d-flex align-items-end">
                    <Textbox placeholder="mesaj" v-model="onWritingMessage" :whenPressEnter="sendMessage"></Textbox>
                    <button class="btn btn_theme" @click="sendMessage"> <font-awesome-icon
                            :icon="['far', 'paper-plane']" /></button>
                </div>
            </div>
            <div v-else-if="screen.width > 768" class="col-8 d-flex justify-content-center align-items-center flex-column">
                <FontAwesomeIcon icon="square-envelope" style="height: 150px;" />
                <span>Arkadaşların ile iletişime geçebilirsin</span>
            </div>
        </div>
    </div>
</template>
<script>
export default {
    data() {
        return {
            messageList: [new Message()],
            images: { fromImage: "", toImage: "" },
            userList: [],
            currentMessagingUser: {},
            connection: null,
            connectionId: null,
            onWritingMessage: "",
            searchText: "",
            searchTextTime: null,
            updateFinishEventList: [],
            observer: null
        }
    },
    methods: {
        sendMessage() {
            this.connection.invoke("createMessage", this.$route.params.id, this.onWritingMessage)
            this.onWritingMessage = "";
        },
        scrollLastMessage() {
            var node = this.$refs.messageList;
            node.scrollTop = node.scrollHeight;
        },
        async refreshModel() {
            var currentToId = this.$route.params.id;
            var currentFromId = this.USER.id;
            if (currentToId) {
                var observer = new IntersectionObserver(
                    async ([e]) => {
                        if (e.isIntersecting) {
                            observer.unobserve(e.target);
                            await this.getOldMessages()
                        }
                    },
                    {
                        root: this.$refs.messageList,
                        rootMargin: "0px 0px",
                        threshold: [1]
                    });
                this.observer = observer;
                if (currentToId == currentFromId) {
                    router.go(-1)
                }
                var { images, messageList } = await this.connection.invoke("GetAll", currentToId);
                this.images = images;
                this.currentMessagingUser = this.userList.find(x => x.id == currentToId);
                this.messageList = messageList;
                this.updateFinishEventList.push(() => {
                    this.observer.observe(this.$refs.messageList.querySelector(":nth-last-child(11)"))
                })

            }
        },
        async getOldMessages() {
            var ary = await this.connection.invoke("GetOldMessages", this.messageList[this.messageList.length - 1].id, this.$route.params.id)
            this.messageList = this.messageList.concat(ary);
            if (ary.length > 0) {
                this.updateFinishEventList.push(() => { this.observer.observe(this.$refs.messageList.querySelector(":nth-last-child(11)")) });
            }
        },
        returnMessageList(){
            router.push("/user/messages")
        }
    },
    async created() {
        const connection = new HubConnectionBuilder().withUrl(this.API_URL + "hub/Message").configureLogging(LogLevel.Information).build();
        await connection.start();
        this.connection = connection;
        this.connectionId = this.connectionId;
        this.userList = await connection.invoke("GetUsers");
        this.connection.on("newMessage", (val) => {
            var currentToId = this.$route.params.id;
            var currentFromId = this.USER.id;
            if ((val.toId == currentToId && val.fromId == currentFromId) || (val.toId == currentFromId && val.fromId == currentToId)) {
                this.messageList.splice(0, 0, new Message(val))
            }
            var userFromUserlist = this.userList.find(x => x.id == val.toId) ?? this.userList.find(x => x.id == val.fromId);
            if (userFromUserlist) {
                userFromUserlist.lastMessage = { text: val.text, date: val.date }
                this.userList.sort((x, y) => {
                    return x.lastMessage.date < y.lastMessage.date;
                })
            }
        })
        await this.refreshModel();
    },
    updated() {
        if (this.updateFinishEventList.length > 0) {
            this.updateFinishEventList.forEach(element => {
                if (typeof (element) == "function") {
                    element()
                }
            });
            this.updateFinishEventList = [];
        }
    },
    watch: {
        searchText(newVal, oldVal) {
            if (this.searchTextTime) {
                clearTimeout(this.searchTextTime);
            }
            this.searchTextTime = setTimeout(() => {
                console.log(this.searchText)
            }, 1000);
        },
        $route(newVal, oldVal) {
            this.scrollLastMessage()
            this.refreshModel();
        }
    }
}
</script>
<style scoped>
.content{
    min-height: 60vh;
    margin-block: 5vh;
}
.write_message_section>div {
    width: calc(100% - 34px);
}

.messaging_person {
    border-bottom: 1px solid var(--pri-border-color);
    padding-bottom: 8px;
}

.messaging_person>img {
    width: 50px;
    height: 50px;
    object-fit: cover;
    border-radius: 50%;
    margin-right: 15px;
}

.friend_list {
    border-right: 1px solid var(--pri-border-color);
}

.friend_list>a {
    display: flex;
    width: 100%;
    color: var(--pri-t-color);
}

.friend_list>a:hover {
    color: var(--pri-t-color) !important;
}

.friend_list>a>img {
    aspect-ratio: 1;
    border-radius: 50%;
    height: 65px;
    width: 65px;
    object-fit: cover;
}

.friend_list>a>div.friend_detail {
    width: calc(100% - 65px);
    padding: 5px;
    padding-left: 15px;
}

.message {
    display: flex;
    padding: 5px;
    align-items: flex-end;
}

.message>img {
    width: 32px;
    height: 32px;
    border-radius: 50%;
    object-fit: cover;
}

.message>span {
    max-width: 75%;
    background-color: rgba(255, 255, 255, 0.2);
    padding: 5px 15px;
    border-radius: 4px;
}

.message.message_send>span {
    margin-right: 11px;
}

.message.message_got>span {
    margin-left: 11px;
}

.message_send {
    justify-content: end;
    flex-direction: row-reverse;
}

.friend_list+div {
    height: 70vh;
}

.message_list {
    height: 60vh;
    flex-direction: column-reverse;
    display: flex;
    overflow-y: scroll;
    overflow-x: hidden;
}

.mobile_return_inbox_btn{
    display: none;
}

@media (max-width:768px) {
    .mobile_return_inbox_btn{
    display: initial;
}
.friend_list{
    border: initial;
}
}
</style>