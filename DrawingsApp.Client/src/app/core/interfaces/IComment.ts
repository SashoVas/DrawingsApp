export interface IComment{
    id:string,
    contents:string,
    sender:{
        senderId:string,
        senderName:string
    },
    postId:string,
    commentedOn:string,
    comments:Array<IComment>
}