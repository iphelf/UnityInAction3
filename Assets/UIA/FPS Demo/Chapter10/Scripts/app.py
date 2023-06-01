from flask import Flask, request

app = Flask(__name__)


@app.post('/')
def echo():
    form = request.form
    try:
        result = f"{form['message']} cloudiness={form['cloud_value']} time={form['timestamp']}\n"
    except ...:
        result = "bad format"
    print(result)
    return result


if __name__ == '__main__':
    app.run()
